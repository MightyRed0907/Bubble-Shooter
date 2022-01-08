using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    [RequireComponent(typeof(AudioSource))]
    public class DelayAndPlay : MonoBehaviour
    {
        public AudioClip[] clip;        
        public Vector2 delayTimeRange = new Vector2(0, 1);

        [Range(0, 1)]
        public float volume = 1;

        private AudioSource AS;

        void Start()
        {
            float delaytime = Random.Range(delayTimeRange.x, delayTimeRange.y);
            AS = GetComponent<AudioSource>();
            StartCoroutine(Play(delaytime));
        }

        IEnumerator Play(float time)
        {
            yield return new WaitForSeconds(time);
            int randindex = Random.Range(0, clip.Length);
            AS.PlayOneShot(clip[randindex], volume);
        }

    }
}
