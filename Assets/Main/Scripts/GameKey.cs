using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKey : Actionable
{
    [HideInInspector]
    public static string DEFAULT = "PASSWORD";
    public string _code = GameKey.DEFAULT;
    // Start is called before the first frame update
    public override void Act()
    {
        PlayerKeyRing.Add(_code);
        Debug.Log("Gained Key: " + _code);
        Destroy(gameObject);
    }
}
