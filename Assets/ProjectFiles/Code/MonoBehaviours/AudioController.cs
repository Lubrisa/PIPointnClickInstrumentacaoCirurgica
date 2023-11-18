using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PointnClick
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;
        private AudioSource m_audioSource;

        private void Awake()
        {
            if (Instance is null)
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
            else if (Instance != this) Destroy(gameObject);
        }

        private void Start() => m_audioSource = GetComponent<AudioSource>();

        public void PlayMusic(AudioClip clip)
        {
            if (clip == m_audioSource.clip) return;

            m_audioSource.clip = clip;
            m_audioSource.Play();
        }
    }
}
