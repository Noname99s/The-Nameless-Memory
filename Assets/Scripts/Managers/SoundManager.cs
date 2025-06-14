using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Orpul1.Sound
{
    public class SoundManager : ObjectPool
    {
        [SerializeField] SoundData[] soundDatas;
        [SerializeField] AudioSource bgmPlayer;
        [SerializeField] AudioListener audioListner;
        [SerializeField] AudioMixer audioMixer;
        protected override void Start() { }
        public void Init()
        {
            Initialize();
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

        public SoundPlayer PlaySound2D(ESoundCategory category, int soundType, EMixerGroup mixerGroup, bool isLoop = false)
        {
            SoundPlayer soundPlayer = GetObject<SoundPlayer>();
            soundPlayer.gameObject.SetActive(true);
            soundPlayer.transform.SetParent(parent.transform);
            soundPlayer.transform.localPosition = Vector3.zero;
            soundPlayer.SetValue(EnqueueObject);
            soundPlayer.SetSound(soundDatas[(int)category].audioList[soundType], audioMixer.FindMatchingGroups(mixerGroup.ToString())[0], 0, isLoop);
            soundPlayer.PlaySound();
            return soundPlayer;
        }

        public SoundPlayer PlaySound3D(ESoundCategory category, int soundType, EMixerGroup mixerGroup, Transform parent, bool isLoop = false)
        {
            SoundPlayer soundPlayer = GetObject<SoundPlayer>();
            soundPlayer.gameObject.SetActive(true);
            soundPlayer.SetValue(EnqueueObject);
            soundPlayer.transform.SetParent(parent);
            soundPlayer.transform.localPosition = Vector3.zero;
            soundPlayer.SetSound(soundDatas[(int)category].audioList[soundType], audioMixer.FindMatchingGroups(mixerGroup.ToString())[0], 1, isLoop);
            soundPlayer.PlaySound();
            return soundPlayer;
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
