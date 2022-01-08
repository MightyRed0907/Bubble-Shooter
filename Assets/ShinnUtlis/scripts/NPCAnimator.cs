using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{

    public class NPCAnimator : MonoBehaviour
    {

        [System.Serializable]
        public struct state
        {
            public string AnimationName;
            public bool active;
        }

        public enum AnimatorType
        {
            Default,
            Random
        }
        private bool TypeSelect()
        {
            switch (animatorType)
            {
                case AnimatorType.Default:
                    return false;
                case AnimatorType.Random:
                    return true;
                default:
                    return false;
            }
        }

        public AnimatorType animatorType;
        public state[] mystate;

        [Space]
        public Animator anim;
        public Vector2 AnimSpeedRange = new Vector2(.75f, 1.25f);

        private bool isplaying = false;
        private bool isdefault = false;
        private float playSpeed;

        void Start()
        {
            playSpeed = Random.Range(AnimSpeedRange.x, AnimSpeedRange.y);

            if (anim == null)
                anim = GetComponent<Animator>();

            if (TypeSelect())
            {
                int index = Random.Range(0, mystate.Length);
                anim.SetBool(mystate[index].AnimationName, true);
            }
            else
            {
                for (int i = 0; i < mystate.Length; i++)
                    if (mystate[i].active)
                        StartCoroutine(DelayPlay(Random.Range(0, 1), mystate[i].AnimationName, playSpeed));
            }
        }

        private void Update()
        {
            if ((anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) && TypeSelect() && !isplaying)
            {
                isplaying = true;
                isdefault = false;

                for (int i = 0; i < mystate.Length; i++)
                    anim.SetBool(mystate[i].AnimationName, false);

            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("default") && !isdefault && TypeSelect())
            {
                isplaying = false;
                isdefault = true;
                int index = Random.Range(0, mystate.Length);
                anim.SetBool(mystate[index].AnimationName, true);
            }
        }

        private IEnumerator DelayPlay(float delay, string state, float speed)
        {
            yield return new WaitForSeconds(delay);
            anim.speed = speed;
            anim.SetBool(state, true);
        }

        public void ForceToDefault()
        {
            AnimatorControllerParameter param;
            for (int i = 0; i < anim.parameters.Length; i++)
            {
                param = anim.parameters[i];
                anim.SetBool(param.name, false);
            }
        }

    }
}
