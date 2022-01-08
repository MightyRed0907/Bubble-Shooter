using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{

    public class ThounderEFX : MonoBehaviour
    {
        public Light m_light;
        public AudioSource m_AS;
        public bool enable = false;
        [Range(0, 3)]
        public float mintime = .5f;
        [Range(0, 1)]
        public float threshold = .5f;

        private float m_lastTime = 0;

        private void Start()
        {
            if (m_light == null)
                m_light = GetComponent<Light>();

            if (m_AS != null)
            {
                m_AS.enabled = false;
                StartCoroutine(ThounderSounds(Random.Range(0, 5)));
            }
        }

        private void Update()
        {
            if (enable)
            {
                if ((Time.time - m_lastTime) > mintime)
                {
                    if (Random.value > threshold)
                        m_light.enabled = true;
                    else
                    {
                        m_light.enabled = false;
                        m_lastTime = Time.time;
                    }
                }
            }

        }

        private IEnumerator ThounderSounds(float delay)
        {
            yield return new WaitForSeconds(delay);
            m_AS.enabled = true;
        }

        [ContextMenu("EnableThunder")]
        public void EnableThunder()
        {
            enable = true;
        }

        [ContextMenu("DisableThunder")]
        public void DisableThunder()
        {
            enable = false;
        }

    }
}