using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
namespace Orpul1.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] SoundData[] soundDatas;
        [SerializeField] AudioSource bgmPlayer;
        [SerializeField] AudioClip[] bgmClipList;
        [SerializeField] List<AudioClip> stageBgm = new List<AudioClip>();
        [SerializeField] AudioListener audioListner;
        [SerializeField] AudioMixer audioMixer;
        [SerializeField] UISoundPlayer uiSoundPlayer;
        Action<int> playerUISound;
        Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
        AudioSource recentAudioSource;
        public void Init()
        {
            SceneManager.sceneLoaded += OnLoadScene;
            for (int i = (int)Stage.Stage1; i <= (int)Stage.Stage2; i++)
            {
                bgmDict.Add(((Stage)i).ToString(), stageBgm[i]);
            }
            for (int i = 0; i < 2; i++)
            {
                GameState eSceneBGM = (GameState)i;
                bgmDict.Add(eSceneBGM.ToString(), bgmClipList[i]);
            }
            bgmPlayer.outputAudioMixerGroup = audioMixer.FindMatchingGroups(EMixerGroup.BGM.ToString())[0];
            playerUISound -= uiSoundPlayer.PlayUISound;
            playerUISound += uiSoundPlayer.PlayUISound;
        }

        public void OnLoadScene(Scene arg, LoadSceneMode mode)
        {
            if (arg.name == GameState.RemindRoom.ToString()) return;
            SetBgmInBattleScene(arg);
            if (!bgmDict.ContainsKey(arg.name)) return;
            SetBgmScene(arg);
        }

        private void SetBgmInBattleScene(Scene arg)
        {
            if (arg.name != GameState.Battle.ToString()) return;
            if (!bgmDict.ContainsKey(Managers.Map.stageType.ToString())) return;
            PlayBGM(bgmDict[Managers.Map.stageType.ToString()]);
        }
        private void SetBgmScene(Scene arg)
        {
            if (arg.name == GameState.Battle.ToString()) return;
            PlayBGM(bgmDict[arg.name]);
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
            playerUISound(uiSoundType);
        }
        public void PlaySound2D(ESoundCategory category, int soundType, EMixerGroup mixerGroup)
        {
            GameObject audioObj = new GameObject();
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerGroup.ToString())[0];
            audioSource.clip = soundDatas[(int)category].audioList[soundType];
            recentAudioSource = audioSource;

            audioSource.Play();
            Destroy(audioObj, soundDatas[(int)category].audioList[soundType].length);
        }
        public void PlaySound3D(ESoundCategory category, int soundType, EMixerGroup mixerGroup, Transform parent)
        {
            GameObject audioObj = new GameObject();
            audioObj.transform.parent = parent;
            audioObj.transform.localPosition = Vector3.zero;
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = 0;
            audioSource.maxDistance = 30;
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerGroup.ToString())[0];
            audioSource.clip = soundDatas[(int)category].audioList[soundType];
            recentAudioSource = audioSource;

            audioSource.Play();
            Destroy(audioObj, soundDatas[(int)category].audioList[soundType].length);
        }
        /// <summary>
        /// 소리 재생 매서드
        /// </summary>
        /// <param name="effect">재생할 효과음</param>
        /// <param name="parent">소리 오브젝트 부모 설정</param>
        /// <param name="spatialBlend">3D 공간 음향 0~1(0 = 2D, 1 = 3D )</param>
        /*public void PlayEffect(AudioClip effect, EMixerGroup mixerGroup = EMixerGroup.Effect, Transform parent = null, float spatialBlend = 0, float minDistance = 0, float maxDistance = 1)
        {
            GameObject audioObj = new GameObject();
            audioObj.transform.parent = parent;
            audioObj.transform.localPosition = Vector3.zero;
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.spatialBlend = spatialBlend;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerGroup.ToString())[0];
            audioSource.clip = effect;
            recentAudioSource = audioSource;

            audioSource.Play();
            Destroy(audioObj, effect.length);
        }*/
        public GameObject PlayEffectLoop(AudioClip effect, EMixerGroup mixerGroup = EMixerGroup.Effect, Transform parent = null, float spatialBlend = 0, float minDistance = 0, float maxDistance = 1)
        {
            GameObject audioObj = new GameObject();
            audioObj.transform.parent = parent;
            audioObj.transform.localPosition = Vector3.zero;
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.spatialBlend = spatialBlend;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(mixerGroup.ToString())[0];
            audioSource.clip = effect;
            audioSource.loop = true;
            recentAudioSource = audioSource;

            audioSource.Play();
            return audioObj;
        }
        public void StopRecentSound()
        {
            if (recentAudioSource == null) return;
            Destroy(recentAudioSource.gameObject);
        }
        public void PlayBGM(AudioClip bgm)
        {
            bgmPlayer.Stop();
            bgmPlayer.clip = bgm;
            bgmPlayer.loop = true;
            bgmPlayer.volume = 0.1f;
            bgmPlayer.Play();
        }
    }

}
