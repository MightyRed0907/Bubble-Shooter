using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shinn
{

    public class ShareMaterialCon : MonoBehaviour
    {

        public enum type
        {
            Color,
            Float
        }
        private void typeSelect()
        {
            switch (mytype)
            {
                case type.Float:
                    iTween.ValueTo(gameObject, iTween.Hash("from", FromAndTo.x, "to", FromAndTo.y, "onupdate", "valueto", "time", time, "delay", delay, "easetype", ease, "looptype", loop, "oncomplete", "complete"));
                    break;
                case type.Color:
                    iTween.ValueTo(gameObject, iTween.Hash("from", FromColor.r, "to", ToColor.r, "onupdate", "valuetoR", "time", time, "delay", delay, "easetype", ease, "looptype", loop, "oncomplete", "complete"));
                    iTween.ValueTo(gameObject, iTween.Hash("from", FromColor.g, "to", ToColor.g, "onupdate", "valuetoG", "time", time, "delay", delay, "easetype", ease, "looptype", loop));
                    iTween.ValueTo(gameObject, iTween.Hash("from", FromColor.b, "to", ToColor.b, "onupdate", "valuetoB", "time", time, "delay", delay, "easetype", ease, "looptype", loop));
                    iTween.ValueTo(gameObject, iTween.Hash("from", FromColor.a, "to", ToColor.a, "onupdate", "valuetoA", "time", time, "delay", delay, "easetype", ease, "looptype", loop));
                    break;

            }
        }
        public type mytype;

        [Header("Material")]
        public Material mat;
        public string property;

        [Header("Itween valueto (from and to)")]
        public Vector2 FromAndTo = new Vector2(0, 1);

        [Space]
        public Color FromColor = Color.black;
        public Color ToColor = Color.white;

        [Space, Range(0, 5)]
        public float delay = 0;
        public float time = 1;
        public iTween.EaseType ease;
        public iTween.LoopType loop;

        [Header("Complete")]
        public UnityEvent events;

        private void OnEnable()
        {
            if (mat == null)
            {
                Debug.LogWarning("Please assign material.");
                return;
            }
            typeSelect();
        }

        private void valueto(float newvalue)
        {
            mat.SetFloat(property, newvalue);
        }

        private void valuetoR(float newvalue)
        {
            Color _matColor = mat.GetColor(property);
            mat.SetColor(property, new Color(newvalue, _matColor.g, _matColor.b, _matColor.a));
        }

        private void valuetoG(float newvalue)
        {
            Color _matColor = mat.GetColor(property);
            mat.SetColor(property, new Color(_matColor.r, newvalue, _matColor.b, _matColor.a));
        }

        private void valuetoB(float newvalue)
        {
            Color _matColor = mat.GetColor(property);
            mat.SetColor(property, new Color(_matColor.r, _matColor.g, newvalue, _matColor.a));
        }

        private void valuetoA(float newvalue)
        {
            Color _matColor = mat.GetColor(property);
            mat.SetColor(property, new Color(_matColor.r, _matColor.g, _matColor.b, newvalue));
        }

        private void complete()
        {
            events.Invoke();
        }

    }

}
