using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    public class FreezeTransform : MonoBehaviour
    {
        public enum UpdateState
        {
            Update,
            FixedUpdate,
            None
        }

        [Header("General")]
        public UpdateState state = UpdateState.FixedUpdate;

        [Header("Position")]
        public bool freezePosX = false;
        public bool freezePosY = false;
        public bool freezePosZ = false;

        public Vector3 LimitPosValue;

        [Header("Rotation")]
        public bool freezeRotX = false;
        public bool freezeRotY = false;
        public bool freezeRotZ = false;

        public Vector3 LimitRotValue;

        [Header("Scale")]
        public bool freezeSclX = false;
        public bool freezeSclY = false;
        public bool freezeSclZ = false;

        public Vector3 LimitSclValue = Vector3.one;

        private float m_rotx;
        private float m_roty;
        private float m_rotz;

        private float m_posx;
        private float m_posy;
        private float m_posz;

        private float m_sclx;
        private float m_scly;
        private float m_sclz;

        private void FixedUpdate()
        {
            if (state == UpdateState.FixedUpdate)
            {
                ///Position
                m_posx = freezePosX ? LimitPosValue.x : transform.localPosition.x;
                m_posy = freezePosY ? LimitPosValue.y : transform.localPosition.y;
                m_posz = freezePosZ ? LimitPosValue.z : transform.localPosition.z;

                transform.localPosition = new Vector3(m_posx, m_posy, m_posz);

                ///Rotation
                m_rotx = freezeRotX ? LimitRotValue.x : transform.localEulerAngles.x;
                m_roty = freezeRotY ? LimitRotValue.y : transform.localEulerAngles.y;
                m_rotz = freezeRotZ ? LimitRotValue.z : transform.localEulerAngles.z;

                transform.localEulerAngles = new Vector3(m_rotx, m_roty, m_rotz);

                ///Scale
                m_sclx = freezeSclX ? LimitSclValue.x : transform.localScale.x;
                m_scly = freezeSclY ? LimitSclValue.y : transform.localScale.y;
                m_sclz = freezeSclZ ? LimitSclValue.z : transform.localScale.z;

                transform.localScale = new Vector3(m_sclx, m_scly, m_sclz);
            }
        }


        private void Update()
        {
            if (state == UpdateState.Update)
            {
                ///Position
                m_posx = freezePosX ? LimitPosValue.x : transform.localPosition.x;
                m_posy = freezePosY ? LimitPosValue.y : transform.localPosition.y;
                m_posz = freezePosZ ? LimitPosValue.z : transform.localPosition.z;

                transform.localPosition = new Vector3(m_posx, m_posy, m_posz);

                ///Rotation
                m_rotx = freezeRotX ? LimitRotValue.x : transform.localEulerAngles.x;
                m_roty = freezeRotY ? LimitRotValue.y : transform.localEulerAngles.y;
                m_rotz = freezeRotZ ? LimitRotValue.z : transform.localEulerAngles.z;

                transform.localEulerAngles = new Vector3(m_rotx, m_roty, m_rotz);

                ///Scale
                m_sclx = freezeSclX ? LimitSclValue.x : transform.localScale.x;
                m_scly = freezeSclY ? LimitSclValue.y : transform.localScale.y;
                m_sclz = freezeSclZ ? LimitSclValue.z : transform.localScale.z;

                transform.localScale = new Vector3(m_sclx, m_scly, m_sclz);
            }
        }

    }
}
