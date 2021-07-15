using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actionable : MonoBehaviour
{
    public virtual void Act(){
        Debug.Log("Actionable");
    }
}
