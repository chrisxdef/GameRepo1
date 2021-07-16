using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Actionable
{
    public bool _closed;

    public float _openAngle = 90f;
    public float _deltaAngle = 90f;
    public override void Act(){
        _closed = !_closed;
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
