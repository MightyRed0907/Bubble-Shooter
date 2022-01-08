using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    public class RotateAround : MonoBehaviour
    {
        public enum RotateType
        {
            Constant,
            Noise
        }
        private float RotTypeSelect()
        {
            switch (rotaround)
            {
                case RotateType.Constant:
                    return Time.deltaTime * Speed;

                case RotateType.Noise:
                    timedata += .1f;
                    return BaseValue + Speed * Mathf.PerlinNoise(timedata * NoiseSpeed, 0.0F);

                default:
                    return Time.deltaTime;
            }
        }


        public RotateType rotaround;

        [Header("Constant"), Range(-100, 100)]
        public float Speed = 1;
        [Header("Noise")]
        public float BaseValue = 0;        
        [Range(0, 1)]
        public float NoiseSpeed = .5f;
        [ReadOnly]
        public float timedata = 0;

        [Header("Lookat")]
        public bool lookat = false;
        public Transform target;

        private void FixedUpdate()
        {
            transform.RotateAround(Vector3.zero, Vector3.up, RotTypeSelect());

            if (lookat)
                transform.LookAt(target);
        }
    }

}
