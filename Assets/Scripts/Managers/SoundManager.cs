using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
namespace Orpul1.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] SoundData[] soundDatas;
        [SerializeField] AudioSource bgmPlayer;
        [SerializeField] AudioListener audioListner;
        [SerializeField] AudioMixer audioMixer;
        [SerializeField] UISoundPlayer uiSoundPlayer;
        Action<int> playerUISound;
        public void Init()
        {
            
        }
        public void DeactiveListner()
        {
            audioListner.enabled = false;
            bgmPlayer.Pause();
        }
        public void ActiveListner()
        {
            audioListner.enabled = true;
        }
        public void PlayUISound(int uiSoundType)
        {
            PlaySound2D(ESoundCategory.UI, uiSoundType, EMixerGroup.UISound);
        }
        public void PlaySound2D(ESoundCategory category, int soundType, EMixerGroup mixerGroup, bool isLoop = false)
        {
            GameObject audioObj = new GameObject();
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.loop = isLoop;
            audioSource.spatialBlend = 0;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerGroup.ToString())[0];
            audioSource.clip = soundDatas[(int)category].audioList[soundType];

            audioSource.Play();
            Destroy(audioObj, soundDatas[(int)category].audioList[soundType].length);
        }
        public void PlaySound3D(ESoundCategory category, int soundType, EMixerGroup mixerGroup, Transform parent, bool isLoop = false)
        {
            GameObject audioObj = new GameObject();
            audioObj.transform.parent = parent;
            audioObj.transform.localPosition = Vector3.zero;
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.loop = isLoop;
            audioSource.spatialBlend = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = 0;
            audioSource.maxDistance = 30;
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerGroup.ToString())[0];
            audioSource.clip = soundDatas[(int)category].audioList[soundType];

            audioSource.Play();
            Destroy(audioObj, soundDatas[(int)category].audioList[soundType].length);
        }
        public void StopBGM()
        {
            bgmPlayer.Stop();
        }
        public void PlayBGM(ESceneType type)
        {
            bgmPlayer.Stop();
            bgmPlayer.clip = soundDatas[(int)ESoundCategory.BGM].audioList[(int)type];
            bgmPlayer.loop = true;
            bgmPlayer.volume = 0.1f;
            bgmPlayer.Play();
        }
    }

}
