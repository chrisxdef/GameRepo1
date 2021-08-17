using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhoneCameraControls : MonoBehaviour
{
    public GameObject _phoneScreenUI;
    public GameObject _cameraImage;
    public GameObject _imageViewer;

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
    private List<string> _imageList = new List<string>();
    private int _imageViewerIndex = 0;
    private bool _viewerIsOpen = false;
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
        } else if(_cameraIsOpen && Input.GetKeyDown(KeyCode.Q)){
            _takePicture = true;
        } if(Input.GetKeyDown(KeyCode.Alpha2)){
            if(_viewerIsOpen) this.CloseImageViewer();
            else this.OpenImageViewer();
        } else if(_viewerIsOpen){
            if(Input.GetKeyDown(KeyCode.Alpha3)){
                this.PrevImage();
            } else if (Input.GetKeyDown(KeyCode.Alpha4)){
                this.NextImage();
            }
        }
    }

    private void OpenCamera(){
        this.CloseImageViewer();
        _cameraIsOpen = true;
        _phoneCamera.enabled = true;
        _cameraImage.SetActive(true);
        _phoneScreenUI.SetActive(true);
        this.EnableBackCamera();
    }

    private void CloseCamera(){
        _cameraIsOpen = false;
        _cameraImage.SetActive(false);
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
        StartCoroutine(this.WaitForPicture());
    }

    private IEnumerator WaitForPicture(){
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
        string filename = string.Format("{0}/{1}{2}.png", 
                              Application.persistentDataPath,  
                              System.DateTime.Now.ToString("MMddHHmmss"),
                              Random.Range(11, 99));
        System.IO.File.WriteAllBytes(filename, bytes);
        _imageList.Add(filename);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
        _cameraImage.SetActive(false);
        yield return new WaitForSeconds(_shutterDelay);
        _takePicture = false;
        _phoneCamera.enabled = true;
        _cameraImage.SetActive(true);
    }

    //Image viewer code -- TODO: Refactor into PhoneHUD, PhoneCamera, and PhoneImageViewer
    private void OpenImageViewer(){
        this.CloseCamera();
        _viewerIsOpen = true;
        _imageViewer.SetActive(true);
        _phoneScreenUI.SetActive(true);
        if(_imageList.Count > 0) _imageViewer.transform.Find("NoImageText").gameObject.SetActive(false);
        this.ViewImage(_imageList.Count -1);
    }

    private void CloseImageViewer(){
        _viewerIsOpen = false;
        _imageViewer.SetActive(false);
        _phoneScreenUI.SetActive(false);
    }

    private void ViewImage(int index){
        if(index >= 0 && index < _imageList.Count){
            RawImage ri = _imageViewer.GetComponent<RawImage>();
            _imageViewerIndex = index;
            string filepath = _imageList[index];
            byte[] bytes;
            bytes = System.IO.File.ReadAllBytes(filepath);
            Debug.Log(string.Format("Vieweing screenshot {0}", filepath));
            Texture2D tex = new Texture2D(1,1);
            tex.LoadImage(bytes);
            ri.texture = tex;
        }
    }

    private void NextImage(){
        this.ViewImage(_imageViewerIndex - 1);
    }
    private void PrevImage(){
        this.ViewImage(_imageViewerIndex + 1);
    }
}
