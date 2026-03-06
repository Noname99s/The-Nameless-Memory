using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionView : MonoBehaviour , IOptionView
{
    [Header("Sound Option")]
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle windowToggle;
    [SerializeField] Button exitOptionButton;

    public event Action<int> OnResolutionChanged;
    public event Action<bool> OnWindowModeChanged;
    public event Action OnExitOption;

    void Start()
    {
        resolutionDropdown.onValueChanged.AddListener((value) => OnResolutionChanged?.Invoke(value));
        windowToggle.onValueChanged.AddListener((isFullScreen) => OnWindowModeChanged?.Invoke(isFullScreen));
        exitOptionButton.onClick.AddListener(() => OnExitOption?.Invoke());
    }
    void OnDestroy()
    {
        resolutionDropdown.onValueChanged.RemoveAllListeners();
        windowToggle.onValueChanged.RemoveAllListeners();
    }
    public void InitResolutionOptions(List<Resolution> resolutions , int currentIndex)
    {
        List<string> options = new List<string>(resolutions.Count);
        for (int i = 0; i < resolutions.Count; i++)
        {
            options.Add($"{resolutions[i].width}x{resolutions[i].height}@{resolutions[i].refreshRateRatio.value}Hz");
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetWindowToggle(bool isFullScreen)
    {
        windowToggle.isOn = !isFullScreen;
    }

    public void SetVolume(bool isSfx)
    {
        if(isSfx)
        {
            audioMixer.SetFloat(EMixerGroup.SFX.ToString(), GetVolume(sfxSlider.value));
        }
        else
        {
            audioMixer.SetFloat(EMixerGroup.BGM.ToString(), GetVolume(bgmSlider.value));
        }
    }    
    private float GetVolume(float value)
    {
        float normalized0to1 = value / 5;
        normalized0to1 = Mathf.Max(0.0001f, normalized0to1);
        return Mathf.Log10(normalized0to1) * 20;
    }
}
