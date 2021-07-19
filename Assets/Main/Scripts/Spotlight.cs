using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : Actionable
{
    public Light _lightSource;
    public float _intensity = 10f;
    public float _modifier = 1f;
    private float _m;
    public bool _on;
    //public float flux;
    public override void Act(){
        _on = !_on;
        _m = _on ? _modifier : 0;
        _lightSource.intensity = _m * _intensity;
        Debug.Log("Spotlight");
    }
}
