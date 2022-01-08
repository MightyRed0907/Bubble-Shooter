using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using EasyButtons;

namespace Shinn{

	public class SceneFadeFuct : MonoBehaviour {

        [Header("FadeFuct (FadeInEnd, FadeOutEnd)")]
        public bool AutoStartFadeout = false;
        public bool AutoStartFadein = false;

        [SerializeField, Range(0, 1)]
        public float r, g, b;

        [Header("FadeIn")]
        public float fadeinDelay;
        public float fadeinTime;

        [Header("FadeOut")]
        public float fadeoutDelay;
        public float fadeoutTime;

        public iTween.EaseType ease;

        [Header("Unity Events")]
        [SerializeField] UnityEvent _event;

        private Color32 guiColor;
        private Texture2D tmp2d;
        private float _alpha;

        private void Start()
        {
            tmp2d = new Texture2D(512, 512);
            for (int y = 0; y < tmp2d.height; y++)
                for (int x = 0; x < tmp2d.width; x++)
                    tmp2d.SetPixel(x, y, Color.white);
            tmp2d.Apply();
        }

        private void OnEnable()
        {
            if (AutoStartFadeout)
                Fadeout();

            if (AutoStartFadein)
                Fadein();
        }

        [Button]
        public void Fadein()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", fadeinTime, "delay", fadeinDelay, "onupdate", "processed2", "easetype", ease, "oncomplete", "complete", "oncompletetarget", gameObject));
        }

        [Button]
        public void Fadeout()
        {
            guiColor = new Color(r, g, b, 0);
            iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", fadeoutTime, "delay", fadeoutDelay, "onupdate", "processed1", "easetype", ease, "oncomplete", "complete", "oncompletetarget", gameObject));
        }

        [Button]
        public void InAndOut()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", fadeinTime, "delay", fadeinDelay, "onupdate", "processed1", "easetype", ease, "oncomplete", "completeInAndOut", "oncompletetarget", gameObject));
        }

        private void OnGUI()
        {
            guiColor = new Color(r, g, b, _alpha);
            GUI.color = guiColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tmp2d);
        }

        private void processed1(float newvalue)
        {
            _alpha = newvalue;
        }

        private void processed2(float newvalue)
        {
            _alpha = newvalue;
        }

        private void complete()
        {
            _event.Invoke();
        }

        private void completeInAndOut()
        {
            Fadeout();
        }

        public void ReLoadLevel(int level){
#pragma warning disable CS0618 // 類型或成員已經過時
            Application.LoadLevel (level);
#pragma warning restore CS0618 // 類型或成員已經過時
        }

        public void Destroy(GameObject temp)
        {
            Destroy(temp);
        }
        
	}
}
