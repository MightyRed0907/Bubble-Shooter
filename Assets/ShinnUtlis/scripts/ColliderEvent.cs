using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shinn {

    [RequireComponent(typeof(Rigidbody))]
    public class ColliderEvent : MonoBehaviour {

        public enum type{
            OnTriggerEnter,
            OnCollisionEnter,
            OnTriggerStay,
            OnCollisionStay,
            OnTriggerExit,
            OnCollisionEXit, 
            None
        }

        public type mytype;
        public string tagname = "TagName";

        private bool triEn = false;
        private bool colEn = false;
        private bool triSt = false;
        private bool colSt = false;
        private bool triEx = false;
        private bool colEx = false;

        #region Events
        public bool EnableBool { get; set; }
        public bool EnableInt { get; set; }
        public bool EnableFloat { get; set; }
        public bool EnableFloatArray { get; set; }
        public bool EnableColor { get; set; }
        public bool EnableVoid { get; set; }
        
        public VoidEvent voidevents;
        public BoolEvent boolevents;
        public IntEvent intevents;
        public FloatEvent floatevents;
        public FloatArrayEvent floatarratevents;
        public ColorEvent colorevents;

        public bool boolvalue;
        public int intvalue;
        public float floatvalue;
        public float[] floatarrayvalue;
        public Color colorvalue;
        #endregion

        private void Start()
        {
            switch (mytype)
            {
                default:
                    triEn = false;
                    colEn = false;
                    triSt = false;
                    colSt = false;
                    triEx = false;
                    colEx = false;
                    break;


                case type.OnTriggerEnter:
                    triEn = true;
                    colEn = false;
                    triSt = false;
                    colSt = false;
                    triEx = false;
                    colEx = false;
                    break;

                case type.OnCollisionEnter:
                    triEn = false;
                    colEn = true;
                    triSt = false;
                    colSt = false;
                    triEx = false;
                    colEx = false;
                    break;

                case type.OnTriggerStay:
                    triEn = false;
                    colEn = false;
                    triSt = true;
                    colSt = false;
                    triEx = false;
                    colEx = false;
                    break;

                case type.OnCollisionStay:
                    triEn = false;
                    colEn = false;
                    triSt = false;
                    colSt = true;
                    triEx = false;
                    colEx = false;
                    break;

                case type.OnTriggerExit:
                    triEn = false;
                    colEn = false;
                    triSt = false;
                    colSt = false;
                    triEx = true;
                    colEx = false;
                    break;

                case type.OnCollisionEXit:
                    triEn = false;
                    colEn = false;
                    triSt = false;
                    colSt = false;
                    triEx = false;
                    colEx = true;
                    break;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == tagname && triEn)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == tagname && colEn)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == tagname && triSt)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }
        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.tag == tagname && colSt)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == tagname && triEx)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.tag == tagname && colEx)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }
    }

}
