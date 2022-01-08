using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

namespace Shinn
{

    public class SingleValueController : MonoBehaviour
    {
        public enum Type{
            Itween,
            PerlinNoise
        }

        public Type type = Type.Itween;

        [Header("float to")]
        public Vector2 valuerange = new Vector2(0, 1);
        public FloatEvent floatevents;
        //public bool isfloatevents = false;

        [Header("ItweenSetting")]
        public GameObject target;
        public bool autoStart = true;
        public float time;
        public float delay = 0;
        public iTween.EaseType ease;
        public iTween.LoopType loop;
        public bool ignoreTimeScalest = false;

        public bool complete = false;
        public UnityEvent unityevent;

        [Header("PerlinNoiseSetting")]
        public float intensity = 1;
        public float basevalue = 0;
        public float noiseSpeed = 1;
        public float randomseed = 0.0F;
        public bool startRand = true;
        public float stopTime = 0;
        bool startperlin = false;
        int mytime = 0;

        void OnEnable()
        {


            if (target == null)
                target = gameObject;


            // if (isfloatevents)

            if(autoStart)
                Go();
        }

        public void Go() {

            if (type == Type.PerlinNoise)
            {
                if(startRand)
                    randomseed = UnityEngine.Random.value;
                startperlin = true;

                if(stopTime!=0)
                    Invoke("StopPerlinNoise", stopTime);
            }

            else
            {
                iTween.ValueTo(target, iTween.Hash("from", valuerange.x, "to", valuerange.y, "onupdate", "floateventsProcess",
                                                    "time", time, "delay", delay,
                                                    "easetype", ease, "looptype", loop,
                                                    "ignoretimescale", ignoreTimeScalest,
                                                    "oncomplete", "Complete", "oncompletetarget", gameObject
                                                ));
            }
        }

        private void Update()
        {
            if (startperlin) {
                float value = basevalue + intensity * Mathf.PerlinNoise(Time.time * noiseSpeed, randomseed);
                floatevents.Invoke(value);
            }
        }

        public void StopPerlinNoise() {
            startperlin = false;
        }


        void floateventsProcess(float newvalue)
        {
            floatevents.Invoke(newvalue);
        }

        void Complete()
        {
            if (complete)
                unityevent.Invoke();
        }

    }

}
