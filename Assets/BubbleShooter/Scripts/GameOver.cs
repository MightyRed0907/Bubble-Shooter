using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public GameObject gameover;
    public Launcher launcher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="bubble")
        {
            gameover.SetActive(true);
            launcher.startShooting = false;
        }
    }

}
