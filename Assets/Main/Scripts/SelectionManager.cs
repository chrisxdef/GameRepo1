using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    private string pickupTag = "Pickup";
    [SerializeField] GameObject pickupText;

    private Transform _selection;

    // Update is called once per frame
    void Update()
    {
        if(_selection != null)
        {
            pickupText.SetActive(false);
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if(selection.CompareTag(pickupTag))
            {
                pickupText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);
                    pickupText.SetActive(false);
                }
                _selection = selection;
            }
        }
    }
}
