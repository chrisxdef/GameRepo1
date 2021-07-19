using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerKeyRing
{
    private static HashSet<string> _keys = new HashSet<string>();

    public static void Add(string key){
        _keys.Add(key);
    }

    public static bool Has(string key){
        return _keys.Contains(key);
    }
}   
