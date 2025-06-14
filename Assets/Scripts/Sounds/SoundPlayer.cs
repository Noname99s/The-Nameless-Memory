using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Orpul1.Sound
{
    public class SoundPlayer : MonoBehaviour, ISetDataAble<Action<GameObject>>
    {
        [SerializeField] AudioSource audioSource;
        Action<GameObject> enqeueActoin;
        float audioDuration = 0;
        bool isLoop = false;
        
        public void SetSound(AudioClip clip, AudioMixerGroup audioMixerGroup, float spatialBlend, bool isLoop = false)
        {
            audioDuration = 0;

            audioSource.loop = isLoop;
            audioSource.spatialBlend = 0;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.outputAudioMixerGroup = audioMixerGroup;

            audioSource.spatialBlend = spatialBlend;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = 0;
            audioSource.maxDistance = 30;
            audioSource.clip = clip;
            this.isLoop = isLoop;
        }
        public void PlaySound()
        {
            audioDuration = audioSource.clip.length + Time.time;
            audioSource.Play();
        }
        public void StopSound()
        {
            audioSource.Stop();
            audioDuration = 0;
            gameObject.SetActive(false);
        }

        public void SetValue(Action<GameObject> enqeueActoin)
        {
            this.enqeueActoin -= enqeueActoin;
            this.enqeueActoin += enqeueActoin;
        }
        private void Update()
        {
            if (isLoop) return;
            if (Time.time < audioDuration) return;

            StopSound();
        }
        private void OnDisable()
        {
            enqeueActoin?.Invoke(gameObject);
        }
    }
}
