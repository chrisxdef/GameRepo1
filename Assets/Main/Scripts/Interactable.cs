using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string _textPrefix = "[F]";
    public string _text = "Use";
    public string interactText{
        get{return _textPrefix + " " + _text;}
        set{_text = value;}
    }
    public List<Actionable> actors;
    
    public void Interact(){
        foreach (Actionable actor in this.actors)
        {
            actor.Act();
        }
    }
}
