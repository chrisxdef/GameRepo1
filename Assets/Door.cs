using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Actionable
{

    public bool closed;

    private float angle = 90f;

    public override void Act(){
        this.closed = !closed;
        Debug.Log("door");
    }

    void Update(){
        float a = this.closed ? 0f : this.angle;
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, a,  this.transform.eulerAngles.z);
    }
}
