using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    
    public void AppReload()
    {
        SceneManager.LoadScene(0);
        print("yes");
    }
     
    public void Debug()
    {
        print("debug");
    }
}
