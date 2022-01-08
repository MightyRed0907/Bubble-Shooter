using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shinn
{
    public class ObjLife : MonoBehaviour
    {
        [Header("Life Cycle")]
        public float LifeTime;
        public Renderer[] fadeObjs;

        [Header("itweenSetting")]
        public iTween.EaseType ease;
        [Range(0, 10)]
        public float time = 1;
        [Range(0, 10)]
        public float delay = 0;

        [Header("Unity Event")]
        public bool completeEvent = false;
        public UnityEvent unityevent;

        private void Awake()
        {
            StartCoroutine(LifeCount(LifeTime));
        }

        private IEnumerator LifeCount(float delay)
        {
            yield return new WaitForSeconds(delay);
            ObjFadeout();
        }

        private void ObjFadeout()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "easetype", ease, "delay", delay, "time", time, "onupdate", "process", "oncomplete", "complete", "oncompletetarget", gameObject));
        }

        private void process(float newvalue)
        {
            for (int i = 0; i < fadeObjs.Length; i++)
                fadeObjs[i].material.color = new Color(fadeObjs[i].material.color.r, fadeObjs[i].material.color.g, fadeObjs[i].material.color.b, newvalue);
        }

        private void complete()
        {
            if (completeEvent)
                unityevent.Invoke();
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
