using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using RenderHeads.Media.AVProVideo;

namespace Shinn
{
    public class SceneTools : MonoBehaviour
    {
        static SceneTools s_Instance;

        [Header("Reload")]
        public KeyCode ReloadKey = KeyCode.F5;
        public bool Dontdestroy = true;
        public int level = 0;

        [Header("Pause")]
        public KeyCode PauseKey = KeyCode.P;
        //private MediaPlayer[] mediaplayer;
        private bool pause = false;

        [Header("Screen Resolutuin setting")]
        public Vector2 ScreenResolution = new Vector2(1920, 281);
        public bool FullScreen = true;
        public bool FitToScreen = false;

        [Header("Show Sys info")]
        public KeyCode ShowKey = KeyCode.S;

        private bool showInfo = false;
        private float updateInterval = 0.5f;
        private float accum = 0.0f;
        private int frames = 0;
        private float timeleft;
        private string fps;

        [Header("Text style")]
        public GUIStyle myStyle;

        protected Rect viewWindow;
        
        private void Awake()
        {
            if (Dontdestroy)
            {
                if (s_Instance == null)
                {
                    s_Instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else if (this != s_Instance)
                {
                    Destroy(gameObject);
                }
            }

            if (FitToScreen)
            {
                Debug.Log("Resolution: " + ScreenResolution.x + " x " + ScreenResolution.y + " FullScreen: " + FullScreen);
                Screen.SetResolution((int)ScreenResolution.x, (int)ScreenResolution.y, FullScreen);
            }
        }

        void Start()
        {
            timeleft = updateInterval;
            viewWindow = new Rect(10, 10, 150, 50);
        
            // Need Avpro Packages
            //var allmedia = Resources.FindObjectsOfTypeAll<MediaPlayer>();
            //mediaplayer = new MediaPlayer[allmedia.Length];
            //mediaplayer = allmedia;
        }

        void Update()
        {
            if (Input.GetKeyDown(PauseKey))
            {
                pause = !pause;

                if (pause)
                    Pause();
                else
                    Resume();
            }

            if (Input.GetKeyDown(ReloadKey))
                SceneManager.LoadScene(level);

            if (Input.GetKeyDown(ShowKey))
                showInfo = !showInfo;

            if (showInfo)
            {
                timeleft -= Time.deltaTime;
                accum += Time.timeScale / Time.deltaTime;
                ++frames;

                if (timeleft <= 0.0)
                {
                    fps = "" + (accum / frames).ToString("f0");
                    timeleft = updateInterval;
                    accum = 0.0f;
                    frames = 0;
                }
            }

        }

        protected void Window(int id)
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Label("FPS                " + fps, myStyle);
                GUILayout.Label("Play time         " + Time.time.ToString("f0"), myStyle);
            }
            UnityEngine.GUI.DragWindow();
        }


        private void OnGUI()
        {
            if(showInfo)
                using (new GUILayout.HorizontalScope())
                {
                    viewWindow = GUILayout.Window(GetInstanceID(), viewWindow, Window, "System Info");
                }
        }


        private void Pause()
        {
            Time.timeScale = 0;

            //if (mediaplayer == null)
            //    return;

            //for (int i = 0; i < mediaplayer.Length; i++)
            //    mediaplayer[i].Pause();
        }

        private void Resume()
        {
            Time.timeScale = 1;

            //if (mediaplayer == null)
            //    return;

            //for (int i = 0; i < mediaplayer.Length; i++)
            //    mediaplayer[i].Play();
        }
    }
}
