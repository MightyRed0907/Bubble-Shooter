using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Shinn
{
    public class UIFade : MonoBehaviour
    {

        public enum Type2D
        {
            Text, Image, Sprite
        }
        private Color TypeSelect(GameObject go)
        {
            switch (type2D)
            {
                default:
                    Image imaged = go.GetComponent<Image>();
                    return imaged.color = new Color(orgColor.r, orgColor.g, orgColor.b, fadeout);
                case Type2D.Text:
                    Text text = go.GetComponent<Text>();
                    return text.color = new Color(orgColor.r, orgColor.g, orgColor.b, fadeout);
                case Type2D.Image:
                    Image image = go.GetComponent<Image>();
                    return image.color = new Color(orgColor.r, orgColor.g, orgColor.b, fadeout);
                case Type2D.Sprite:
                    SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
                    return sprite.color = new Color(orgColor.r, orgColor.g, orgColor.b, fadeout);
            }
        }

        public enum Function
        {
            Breath, FadeIn, FadeOut
        }
        private void FunctionSelect()
        {
            switch (function)
            {
                default:
                    break;

                case Function.Breath:
                    if (breathin)
                    {
                        if (fadeout > AlphaUpperLower.y)
                        {
                            fadeout = AlphaUpperLower.y;
                            breathout = true;
                            breathin = false;
                        }
                        else
                            fadeout += speed * Time.fixedDeltaTime;

                        TypeSelect(fadeobj);
                    }

                    else if (breathout)
                    {
                        if (fadeout < AlphaUpperLower.x)
                        {
                            fadeout = AlphaUpperLower.x;
                            breathout = false;
                            breathin = true;
                        }
                        else
                            fadeout -= speed * Time.fixedDeltaTime;

                        TypeSelect(fadeobj);
                    }
                    break;

                case Function.FadeIn:

                    if (breathin)
                    {
                        if (fadeout > AlphaUpperLower.y)
                        {
                            fadeout = AlphaUpperLower.y;
                            breathin = false;
                            completeEvents.Invoke();
                        }
                        else
                            fadeout += speed * Time.fixedDeltaTime;

                        TypeSelect(fadeobj);
                    }
                    break;

                case Function.FadeOut:
                    if (breathout)
                    {
                        if (fadeout < AlphaUpperLower.x)
                        {
                            fadeout = AlphaUpperLower.x;
                            breathout = false;
                            completeEvents.Invoke();
                        }
                        else
                            fadeout -= speed * Time.fixedDeltaTime;

                        TypeSelect(fadeobj);
                    }
                    break;
            }
        }


        public Type2D type2D;
        public Function function;

        public GameObject fadeobj;

        [Header("Setting"), Range(0, 2)]
        public float speed = .5f;
        public Color orgColor = Color.white;
        public Vector2 AlphaUpperLower = Vector2.one;

        [Header("Events")]
        public UnityEvent completeEvents;

        private float fadeout = 0;
        private bool breathin = true;
        private bool breathout = false;

        private void OnEnable()
        {
            if (fadeobj == null)
                fadeobj = gameObject;

            if (function == Function.Breath)
            {
                fadeout = 0;
                breathin = true;
                breathout = false;
            }
            if (function == Function.FadeIn)
            {
                fadeout = 0;
                breathin = true;
            }
            if (function == Function.FadeOut)
            {
                fadeout = 1;
                breathout = true;
            }
        }

        private void FixedUpdate()
        {
            FunctionSelect();
        }
    }
}
