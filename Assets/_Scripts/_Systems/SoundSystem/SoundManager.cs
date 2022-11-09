using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Project.Systems.SoundSystem
{
    public class SoundManager : MonoBehaviour
    {
        public void PlaySoundEffect(GameObject parent, AudioClip ac=null)
        {
            if (ac is null) return;
            var audioSource = parent.GetComponentInChildren<AudioSource>();
            if (audioSource is null)
            {
                var audioObj = Instantiate(new GameObject("Sound Effect", typeof(AudioSource)), parent.transform.position, quaternion.identity);
                audioObj.transform.SetParent(parent.transform);
                audioSource = audioObj.GetComponent<AudioSource>();
            }
            
            if (audioSource.isPlaying) return;

            audioSource.PlayOneShot(ac);
        }
    }
}