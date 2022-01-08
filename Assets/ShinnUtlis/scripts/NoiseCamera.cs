using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
	public class NoiseCamera : MonoBehaviour {

		[Header("NoiseCamera")]
        public Camera cam;

        public bool autoStart = true;
		public bool NoiseCamViewSt {
			get{ return autoStart; }
			set{ autoStart = value; }
		}

		[Space]
        public float BaseValue = 0;
        public float ScaleValue = 30;
        [Range(0, 3)]
        public float NoiseSpeed = .5f;

        [Header("Noise Time Data"), ReadOnly]
        public float timedata;  

        private void Start()
        {
            if (cam == null)
                cam = GetComponent<Camera>();
        }

        private void FixedUpdate ()
        {
			if (autoStart)
            {
				timedata += .01f;
                float zoomindata = BaseValue + Mathf.PerlinNoise (timedata * NoiseSpeed, 0.0F) * ScaleValue;
                cam.fieldOfView = zoomindata;
			}

		}
	}

}
