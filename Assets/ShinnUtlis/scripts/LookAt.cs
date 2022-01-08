using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] public Transform target;
        [SerializeField] public float speed = .01f;
        [SerializeField] public bool freezeXZ = false;

        private void FixedUpdate()
        {
            Vector3 direction = target.position - transform.position;

            if (freezeXZ)
                direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed);
        }
    }

}
