using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactText = "Use";
    public List<Actionable> actors;
    
    public void Interact(){
        foreach (Actionable actor in this.actors)
        {
            actor.Act();
        }
    }
}
