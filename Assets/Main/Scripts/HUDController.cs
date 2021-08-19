using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject _systemMenu;

    // Start is called before the first frame update
    void Start()
    {
        this.OpenSystemMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(_systemMenu.activeSelf){
                this.CloseSystemMenu();
            } else {
                this.OpenSystemMenu();
            }
        }
        
    }

    public void OpenSystemMenu(){
        _systemMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseSystemMenu(){
        _systemMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
