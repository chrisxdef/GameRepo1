using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Actionable
{
    public bool _closed;
    private GameLock _lock;
    private Interactable _interactable;
    public float _openAngle = 90f;
    public float _deltaAngle = 90f;

    void Start(){
        _interactable = gameObject.GetComponent<Interactable>();
        // Check if door has lock
        _lock = gameObject.GetComponent<GameLock>();
        if(_lock && _lock.Locked){
            _closed = true;
            if(_interactable) _interactable.interactText = "Locked";
        }
    }

    public override void Act(){
        // Check if has locked and closed.
        if(_lock && _lock.Locked && _closed){
            bool unlocked = _lock.Unlock();
            if(!unlocked) return;
        } else {
            _closed = !_closed;
        }
        if(_interactable) _interactable.interactText = _closed ? "Open" : "Close";
        Debug.Log("door");
    }

    void Update(){
        float y = this.transform.eulerAngles.y;
        if(_closed && y > 0f || !_closed && y < 90f){
            float angle = _deltaAngle*Time.deltaTime;
            if(_closed) angle *= -1.0f;
            float s = Mathf.Clamp(y + angle, 0f, _openAngle);
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, s, this.transform.eulerAngles.z);   
        }
    }
}
