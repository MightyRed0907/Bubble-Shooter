using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public void AppReload()
    {
        SceneManager.LoadScene(0);
    }
     
    public void Debug()
    {
        print("debug");
    }
}
