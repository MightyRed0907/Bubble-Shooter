using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{

    public class Follower : MonoBehaviour
    {
        public enum MoveType
        {
            Lerp,
            Translate
        }

        [Header("Chase Target")]
        public bool enable = true;
        public Transform target;
        public MoveType type;

        public bool Enable { set { enable = value; } }
        public Transform Target { set { target = value; } }

        [Header("Chase speed and rotation speed."), Range(0, 1)]
        public float chaseSpeed = .1f;
        [Range(0, 1)]
        public float rotationSpeed = .1f;

        [Header("Stop distance"), Range(0, 10)]
        public float stopDist = 2;

        [Header("Freeze RotY")]
        public bool onTheGround = false;

        [Header("Animator")]
        public bool enableAnimation = false;
        public Animator anim;
        public string moveAnimName = "walk";
        public string encounterAnimName = "attack";

       private bool MoveTypeState()
        {
            switch (type)
            {
                case MoveType.Lerp:
                    return true;

                case MoveType.Translate:
                    return false;

                default:
                    return false;
            }
        }

        void FixedUpdate()
        {
            if (enable)
            {
                Vector3 direction = target.transform.position - transform.position;

                if (onTheGround)
                    direction.y = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);

                if (direction.magnitude > stopDist)
                {
                    if (enableAnimation)
                    {
                        if (anim == null)
                        {
                            Debug.LogError("Need assign an animator.");
                            return;
                        }
                        else
                        {
                            if (anim.GetCurrentAnimatorStateInfo(0).IsName(moveAnimName))
                            {
                                anim.SetBool(moveAnimName, true);
                                anim.SetBool(encounterAnimName, false);

                                if (MoveTypeState())
                                    transform.position = Vector3.Lerp(transform.position, target.position, chaseSpeed);
                                else
                                    transform.Translate(0, 0, chaseSpeed);
                            }
                        }
                    }
                    else
                    {
                        if (MoveTypeState())
                            transform.position = Vector3.Lerp(transform.position, target.position, chaseSpeed);
                        else
                            transform.Translate(0, 0, chaseSpeed);
                    }
                }
                else
                {
                    if (enableAnimation)
                    {
                        if (anim == null)
                        {
                            Debug.LogError("Need assign an animator.");
                            return;
                        }
                        else
                        {
                            anim.SetBool(moveAnimName, false);
                            anim.SetBool(encounterAnimName, true);
                        }
                    }
                }


            }

        }
    }

}
