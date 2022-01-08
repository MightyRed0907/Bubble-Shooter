using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn{

	public class RotateSelf : MonoBehaviour
    {

        public enum Type
        {
            Constant,
            Random,
            Noise
        }
        public void TypeSelect()
        {
            switch (type)
            {
                case Type.Constant:
                    transform.Rotate(new Vector3(px, py, pz) * speed);
                    break;
                case Type.Random:
                    transform.Rotate(new Vector3(px, py, pz) * speed);
                    break;
                case Type.Noise:
                    transform.Rotate(new Vector3(
                                BaseValue.x + speed * Mathf.PerlinNoise(Time.time * NoiseSpeed, NoiseSeed1),
                                BaseValue.y + speed * Mathf.PerlinNoise(Time.time * NoiseSpeed, NoiseSeed2),
                                BaseValue.z + speed * Mathf.PerlinNoise(Time.time * NoiseSpeed, NoiseSeed3)
                    ));
                    break;
                default:
                    break;
            }
        }
        public Type type;

        [SerializeField, Range(0, 3)] float speed = .5f;

        [Header("Constant Rotate")]
		[SerializeField, Range(-3, 3)] float px;
		[SerializeField, Range(-3, 3)] float py;
		[SerializeField, Range(-3, 3)] float pz;		

		[Header("Random Rotate")]
		[SerializeField] Vector2 RotatePxRange;
		[SerializeField] Vector2 RotatePyRange;
		[SerializeField] Vector2 RotatePzRange;

        [Header("Noise Rotate")]
        [SerializeField] Vector3 BaseValue;
        [SerializeField, Range(0, 1)] float NoiseSpeed=1;

        [Header("Freeze Rotation")]
		[SerializeField] bool FreezeRotx = false;
		[SerializeField] bool FreezeRoty = false;
		[SerializeField] bool FreezeRotz = false;

        private float NoiseSeed1;
        private float NoiseSeed2;
        private float NoiseSeed3;
        private float RotxOrg;
        private float RotyOrg;
        private float RotzOrg;

        private void Start()
        {            
            RotxOrg = transform.localEulerAngles.x;
            RotyOrg = transform.localEulerAngles.y;
            RotzOrg = transform.localEulerAngles.z;

            if (type == Type.Random)
            {
                px = Random.Range(RotatePxRange.x, RotatePxRange.y);
                py = Random.Range(RotatePyRange.x, RotatePyRange.y);
                pz = Random.Range(RotatePzRange.x, RotatePzRange.y);
            }
            else if (type == Type.Noise)
            {
                NoiseSeed1 = Random.value;
                NoiseSeed2 = Random.value;
                NoiseSeed3 = Random.value;
            }
        }

        private void FixedUpdate () {

            TypeSelect();

			if (FreezeRotx)
				transform.localEulerAngles = new Vector3 (RotxOrg, transform.localEulerAngles.y, transform.localEulerAngles.z);

			if (FreezeRoty)
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, RotyOrg, transform.localEulerAngles.z);

			if (FreezeRotz)
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, RotzOrg);
		}

	}
}
