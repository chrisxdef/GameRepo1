using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private string pickupTag = "Pickup";

    private Transform _selection;

    public float interactDistance = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(_selection != null)
        {
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            /*
            var selection = hit.transform;
            if(selection.CompareTag(pickupTag))
            {
                pickupText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);
                }
                _selection = selection;
            }
            */
            
            // check for interactable
            if(hit.distance < interactDistance){
                Interactable i = hit.collider.GetComponent<Interactable>();
                if(i != null){
                    if(Input.GetKeyDown(KeyCode.F)){
                        i.Interact();
                    }
                }
            }
           
        }
    }
}
