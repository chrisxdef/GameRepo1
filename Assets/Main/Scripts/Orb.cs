using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Actionable
{
    private Material _Material;
    void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        _Material = GetComponent<Renderer>().material;
        _Material.color = Color.red;
    }

    public override void Act(){
        if(_Material.color == Color.red){
            _Material.color = Color.blue;
        } else {
            _Material.color = Color.red;
        }
        Debug.Log("orb");
    }
}
