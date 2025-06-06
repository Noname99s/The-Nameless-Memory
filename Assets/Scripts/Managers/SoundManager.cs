using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum EMixerGroup
{
    Master,
    Background,
    Effect,
    Object,
    Player,
    Enemy,
    UISound
}
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmPlayer;
    [SerializeField] AudioClip[] bgmClipList;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] UISoundPlayer uiSoundPlayer;
    Action<EUiSoundType> playerUISound;
    Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    AudioSource recentAudioSource;
    public void Init()
    {
        SceneManager.sceneLoaded += OnLoadScene;
        for (int i = 0; i < (int)GameState.Ending; i++)
        {
            GameState eSceneBGM = (GameState)i;
            bgmDict.Add(eSceneBGM.ToString(), bgmClipList[i]);
        }
        bgmPlayer.outputAudioMixerGroup = audioMixer.FindMatchingGroups(EMixerGroup.Background.ToString())[0];
        playerUISound -= uiSoundPlayer.PlayUISound;
        playerUISound += uiSoundPlayer.PlayUISound;
    }

    public void OnLoadScene(Scene arg, LoadSceneMode mode)
    {
        if (!bgmDict.ContainsKey(arg.name)) return;
        PlayBGM(bgmDict[arg.name]);
    }
    public void PlayUISound(EUiSoundType uiSoundType)
    {
        playerUISound(uiSoundType);
    }
    /// <summary>
    /// 소리 재생 매서드
    /// </summary>
    /// <param name="effect">재생할 효과음</param>
    /// <param name="parent">소리 오브젝트 부모 설정</param>
    /// <param name="spatialBlend">3D 공간 음향 0~1(0 = 2D, 1 = 3D )</param>
    public void PlayEffect(AudioClip effect, EMixerGroup mixerGroup = EMixerGroup.Effect, Transform parent = null, float spatialBlend = 0,float minDistance = 0, float maxDistance = 1)
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
    }
    public void StopRecentSound()
    {
        if (recentAudioSource == null) return;
        Destroy(recentAudioSource.gameObject);
    }
    public void PlayBGM(AudioClip bgm)
    {
        bgmPlayer.clip = bgm;
        bgmPlayer.loop = true;
        bgmPlayer.volume = 0.1f;
        bgmPlayer.Play();
    }
}
