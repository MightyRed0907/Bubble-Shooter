using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public void AppReload()
    {
        Application.LoadLevel(0);
    }
     
    public void Debug()
    {
        print("debug");
    }
}
