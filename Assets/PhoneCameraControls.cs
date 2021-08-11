using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCameraControls : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _phoneScreenUI;
    private bool _cameraIsOpen;
    private Camera _phoneCamera;
    private KeyCode _enableCameraKey = KeyCode.Alpha1;
    private KeyCode _swapCameraView = KeyCode.Tab; 
    private bool _frontCameraEnabled;
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
        }
    }

    void OpenCamera(){
        _cameraIsOpen = true;
        _phoneCamera.enabled = true;
        _phoneScreenUI.SetActive(true);
        this.EnableBackCamera();
    }

    void CloseCamera(){
        _cameraIsOpen = false;
        _phoneCamera.enabled = false;
        _phoneScreenUI.SetActive(false);
    }

    void EnableBackCamera(){
        this.transform.localEulerAngles = _backCameraTransform;
        _frontCameraEnabled = false;
    }
    void EnableFrontCamera(){
        this.transform.localEulerAngles = _frontCameraTransform;
        _frontCameraEnabled = true;
    }
}
