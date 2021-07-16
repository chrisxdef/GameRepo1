using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    private Transform _selection;

    public float _interactDistance = 1.0f;
    
    public Text _interactableText;
    int _interactableLayerMask = 0;

    void Start(){
        int defaultLayer = LayerMask.GetMask("Default");
        int groundLayer = LayerMask.GetMask("Ground");
        _interactableLayerMask = defaultLayer | groundLayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(_selection != null)
        {
            _interactableText.text = " ";
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _interactDistance, _interactableLayerMask))
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
            var selection = hit.transform;
            Interactable i = selection.GetComponent<Interactable>();
            if(i != null){
                _interactableText.text = i.interactText;
                if(Input.GetKeyDown(KeyCode.F)){
                    i.Interact();
                }
            }
            _selection = selection;
        }
    }
}
