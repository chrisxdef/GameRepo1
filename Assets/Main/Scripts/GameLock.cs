using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLock : MonoBehaviour
{
    private bool _locked;
    public bool Locked{
        get{return _locked;}
    }
    public string _code;
    public bool _startLocked;
    // Start is called before the first frame update
    void Start()
    {
        if(_code==""){
            _code = GameKey.DEFAULT;
        }
        _locked = _startLocked;
    }
    public bool KeyCheck(){
        return PlayerKeyRing.Has(_code);
    }

    public bool Lock(){
        if(_locked) return true;
        else if(KeyCheck()){
            _locked = true;
            return true;
        }
        return false;
    }

    public bool Unlock(){
        if(!_locked) return true;
        else if(KeyCheck()){
            _locked = false;
            return true;
        }
        return false;
    }
}
