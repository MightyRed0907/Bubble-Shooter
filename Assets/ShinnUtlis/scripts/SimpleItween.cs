using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shinn
{

    public class SimpleItween : MonoBehaviour
    {

        #region Itween
        public enum state
        {
            shakePosition,
            punchPosition,
            scaleTo,
            moveTo,
            rotationTo,
            SP_fadeTo,
            colorTo,
            rotationToAndMoveTo,
        }

        public state mystate;

        [Header("ItweenSetting")]
        public GameObject target;
        public float time;
        public float delay = 0;
        public iTween.EaseType ease;
        public iTween.LoopType loop;
        public bool islocal = false;
        public bool ignoreTimeScalest = false;
        public bool AutoStart = true;
        public bool orienttopathst = false;
        public float lookaheadValue = .05f;

        [Header("ShakePosition")]
        public Vector3 shakePos;

        [Header("PunchPosition")]
        public Vector3 punchPos;

        [Header("ScaleState")]
        public Vector3 scaleValue;

        [Header("MoveTo")]
        public Transform moveloc;

        [Header("RotationTo")]
        public Vector3 rotvalue;

        [Header("ColorTo")]
        public Color startColor;
        public Color endColor;

        [Header("FadeTo")]
        public float fadeStart = 0;
        public float fadeEnd = 1;

        [Header("CompleteEvent")]
        public bool startComplete = false;

        #endregion

        #region UnityEvents
        public bool EnableBool = false;
        public bool EnableInt = false;
        public bool EnableFloat = false;
        public bool EnableFloatArray = false;
        public bool EnableVector3 = false;
        public bool EnableColor = false;
        public bool EnableVoid = false;

        public VoidEvent voidevents;
        public BoolEvent boolevents;
        public IntEvent intevents;
        public FloatEvent floatevents;
        public FloatArrayEvent floatarratevents;
        public Vector3Event vector3events;
        public ColorEvent colorevents;

        public bool boolvalue;
        public int intvalue;
        public float floatvalue;
        public float[] floatarrayvalue;
        public Vector3 vector3value;
        public Color colorvalue;
        #endregion
        
        private void OnEnable()
        {

            if (target == null)
                target = gameObject;

            if (AutoStart)
                Select();
        }

        public void CallStart()
        {
            Select();
        }

        public void Pause()
        {
            iTween.Pause(gameObject);
        }

        public void Resume()
        {
            iTween.Resume(gameObject);
        }

        public void Stop()
        {
            iTween.Stop(gameObject);
        }

        private void Select()
        {
            switch (mystate)
            {
                case state.shakePosition:
                    iTween.ShakePosition(target, iTween.Hash("x", shakePos.x, "y", shakePos.y, "z", shakePos.z,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));
                    break;

                case state.punchPosition:
                    iTween.PunchPosition(target, iTween.Hash("x", punchPos.x, "y", punchPos.y, "z", punchPos.z,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;

                case state.SP_fadeTo:
                    iTween.ValueTo(target, iTween.Hash("from", fadeStart, "to", fadeEnd, "onupdate", "fadeto1",
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;

                case state.scaleTo:
                    iTween.ScaleTo(target, iTween.Hash("scale", scaleValue,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;

                case state.moveTo:
                    if (islocal)
                        iTween.MoveTo(target, iTween.Hash("position", moveloc.localPosition,
                                                                    "time", time, "delay", delay,
                                                                    "easetype", ease, "looptype", loop,
                                                                    "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                    "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                     "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));
                    else
                        iTween.MoveTo(target, iTween.Hash("position", moveloc.position,
                                                                    "time", time, "delay", delay,
                                                                    "easetype", ease, "looptype", loop,
                                                                    "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                    "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                    "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));

                    break;


                case state.rotationTo:
                    iTween.RotateTo(target, iTween.Hash("rotation", rotvalue,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;



                case state.colorTo:
                    iTween.ColorTo(target, iTween.Hash("color", endColor,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;


                case state.rotationToAndMoveTo:

                    if (islocal)
                    {
                        iTween.MoveTo(target, iTween.Hash("position", moveloc.localPosition,
                                                                    "time", time, "delay", delay,
                                                                    "easetype", ease, "looptype", loop,
                                                                    "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                    "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                     "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));

                        iTween.RotateTo(target, iTween.Hash("rotation", moveloc.localEulerAngles,
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    }
                    else
                    {
                        iTween.MoveTo(target, iTween.Hash("position", moveloc.position,
                                                                    "time", time, "delay", delay,
                                                                    "easetype", ease, "looptype", loop,
                                                                    "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                    "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                    "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));

                        iTween.RotateTo(target, iTween.Hash("rotation", moveloc.eulerAngles,
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                       ));
                    }

                    break;


                default:
                    break;
            }
        }

        private void Fadeto1(float newvalue)
        {
            if (GetComponent<SpriteRenderer>() != null)
            {
                SpriteRenderer sp = GetComponent<SpriteRenderer>();
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, newvalue);
            }
        }

        private void Complete()
        {
            if (startComplete)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableVector3)
                    vector3events.Invoke(vector3value);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }

        private void OnDisable()
        {
            var itween = GetComponent<iTween>();
            if (itween != null)
                Destroy(itween);
        }

    }

}
