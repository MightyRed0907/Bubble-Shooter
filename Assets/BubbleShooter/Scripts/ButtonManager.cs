using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : Singleton<ButtonManager>
{
    public int Difficulty { set; get; }
    
    [SerializeField]
    public HashSet<char> Level { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Level = new HashSet<char>();
        Level.Add('+');
        Level.Add('-');
        Level.Add('x');
        Level.Add('÷');
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void EasyStart()
    {
        Difficulty = 1;
        PlayerPrefs.SetInt("difficulty", Difficulty);
        SceneManager.LoadSceneAsync(2);
    }

    public void MediumStart()
    {
        Difficulty = 2;
        PlayerPrefs.SetInt("difficulty", Difficulty);
        SceneManager.LoadSceneAsync(2);
    }
    public void HardStart()
    {
        Difficulty = 3;
        PlayerPrefs.SetInt("difficulty", Difficulty);
        SceneManager.LoadSceneAsync(2);
    }
    public void PlusSelect()
    {
        if(Level.Contains('+'))
            Level.Remove('+');
        else
            Level.Add('+');
    }
    public void MinusSelect()
    {
        if (Level.Contains('-'))
            Level.Remove('-');
        else
            Level.Add('-');
    }
    public void MulSelect()
    {
        if (Level.Contains('x'))
            Level.Remove('x');
        else
            Level.Add('x');
    }
    public void DivSelect()
    {
        if (Level.Contains('÷'))
            Level.Remove('÷');
        else
            Level.Add('÷');
    }

    public void SettingDone()
    {
        char[] temp = new char[Level.Count];
        Level.CopyTo(temp);
        Debug.Log(new string(temp));
        PlayerPrefs.SetString("level", new string(temp));
        
        SceneManager.LoadSceneAsync(3);
    }

    public void Back()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
