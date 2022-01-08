using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    public class NPCBehavior : MonoBehaviour
    {
        [Header("NPC Moving Speed")]
        public bool autoStart = true;
        public bool freezeXZ = true;
        [Range(0, 1)]
        public float ChaseSpeed = .1f;
        [Range(0.1f, 5)]
        public float stopDist = 1;        

        [Header("Moving Range")]
        public Vector2 MoveRange = new Vector2(-10, 10);

        [Header("Rest Time")]
        public Vector2 RestTimeRange = new Vector2(0, 3);

        [Header("Show Gizmos")]
        public bool showTarget = true;

        private Vector3 target;
        private float timevalue;
        private float RestTime;

        private void Start()
        {
            target = transform.position;
            RestTime = Random.Range(RestTimeRange.x, RestTimeRange.y);
        }

        private void FixedUpdate()
        {
            if (autoStart)
            {
                Vector3 direction = target - transform.position;
                if (freezeXZ)
                    direction.y = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), .1f);

                if (direction.magnitude > stopDist)
                    transform.Translate(0, 0, ChaseSpeed);

                else
                {
                    timevalue += Time.fixedDeltaTime;
                    int seconds = (int)timevalue % 60;

                    if (seconds > RestTime)
                    {
                        timevalue = 0;
                        RestTime = Random.Range(RestTimeRange.x, RestTimeRange.y);

                        if (freezeXZ)
                            target = new Vector3(   transform.position.x + Random.Range(MoveRange.x, MoveRange.y), 
                                                    0, 
                                                    transform.position.z + Random.Range(MoveRange.x, MoveRange.y));
                        else
                            target = new Vector3(   transform.position.x + Random.Range(MoveRange.x, MoveRange.y), 
                                                    transform.position.y + Random.Range(MoveRange.x, MoveRange.y), 
                                                    transform.position.z + Random.Range(MoveRange.x, MoveRange.y));
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (showTarget)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(target, .5f);
            }
        }

    }
}
