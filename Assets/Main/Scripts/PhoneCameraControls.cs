using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhoneCameraControls : MonoBehaviour
{
    public GameObject _phoneScreenUI;
    public GameObject _cameraImage;
    [Range(0.1f, 1.0f)]
    public float _shutterDelay;
    private Camera _phoneCamera; // assigned in Start()
    private bool _cameraIsOpen = false;
    private bool _takePicture = false;
    private bool _frontCameraEnabled = false;
    private KeyCode _enableCameraKey = KeyCode.Alpha1;
    private KeyCode _swapCameraView = KeyCode.Tab; 
    private Vector3 _frontCameraTransform = new Vector3(180.0f, 0f, 180.0f);
    private Vector3 _backCameraTransform = Vector3.zero;
    void Start()
    {
        _phoneCamera = GetComponent<Camera>();
        this.CloseCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_enableCameraKey)){
            if(_cameraIsOpen) this.CloseCamera();
            else this.OpenCamera();
        } else if(_cameraIsOpen && Input.GetKeyDown(_swapCameraView)){
            if(_frontCameraEnabled) this.EnableBackCamera();
            else this.EnableFrontCamera();
        } else if(_cameraIsOpen && Input.GetKeyDown(KeyCode.F)){
            _takePicture = true;
        }
    }

    private void OpenCamera(){
        _cameraIsOpen = true;
        _phoneCamera.enabled = true;
        _phoneScreenUI.SetActive(true);
        this.EnableBackCamera();
    }

    private void CloseCamera(){
        _cameraIsOpen = false;
        _phoneCamera.enabled = false;
        _phoneScreenUI.SetActive(false);
    }

    private void EnableBackCamera(){
        this.transform.localEulerAngles = _backCameraTransform;
        _frontCameraEnabled = false;
    }
    private void EnableFrontCamera(){
        this.transform.localEulerAngles = _frontCameraTransform;
        _frontCameraEnabled = true;
    }
    private void TakePicture(){
        _takePicture = true;
    }

    void OnPostRender() {
        if(!_takePicture) return;

        _phoneCamera.enabled = false;

        RenderTexture activeTexture = _phoneCamera.activeTexture;
        RenderTexture.active = activeTexture;
        int resWidth = 800;
        int resHeight = 420;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        RenderTexture.active = null;
        activeTexture = null;
        
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = string.Format("{0}/screenshots/{1}{2}.png", 
                              Application.dataPath, 
                              Random.Range(11, 99), 
                              System.DateTime.Now.ToString("MMddHHmmss"));
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        StartCoroutine(this.WaitForPicture());
    }

    private IEnumerator WaitForPicture(){
        _cameraImage.SetActive(false);
        yield return new WaitForSeconds(_shutterDelay);
        _takePicture = false;
        _phoneCamera.enabled = true;
        _cameraImage.SetActive(true);
    }
}
