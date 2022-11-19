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

        private void Start()
        {
            EffectSourceSpatialSetup();
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