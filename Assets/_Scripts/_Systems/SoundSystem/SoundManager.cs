using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Systems.SoundSystem
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource universalEffectSource;

        private AudioClip ac_ambient; // ac prefix for audio clip 
        
        private void Start()
        {
            EffectSourceSpatialSetup();
            PlayMusic();
        }

        private void PlayMusic()
        {
            ac_ambient = Resources.Load("underwater") as AudioClip;
            musicSource.clip = ac_ambient;
            musicSource.Play();
        }

        public void PlaySoundEffect(GameObject parent, AudioClip ac=null)
        {
            if (ac is null) return;
            if (universalEffectSource.isPlaying) return;

            universalEffectSource.transform.position = parent.transform.position;
            
            universalEffectSource.PlayOneShot(ac);
        }

        private void EffectSourceSpatialSetup()
        {
            universalEffectSource.spatialize = true;
            universalEffectSource.spatialBlend = 1.0f;
        }
    }
}