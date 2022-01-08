using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    [RequireComponent(typeof(AudioSource))]
    public class CarouselSFX : MonoBehaviour
    {

        [SerializeField] AudioClip[] clip;
        private AudioSource AS { get; set; }

        [SerializeField]
        public bool autoStart = true;

        [Range(0, 1)]
        public float volume = 1;

        private void Start()
        {
            AS = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!AS.isPlaying && autoStart)
            {
                int index = Random.Range(0, clip.Length);
                AS.PlayOneShot(clip[index], volume);
            }
        }

        public void Stop()
        {
            autoStart = false;
        }
    }

}
