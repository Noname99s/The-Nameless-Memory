using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionView : UIPopup , IOptionView
{
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
    public void InitResolutionOptions(Resolution[] resolutions)
    {
        List<string> options = new List<string>(resolutions.Length);
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add($"{resolutions[i].width}x{resolutions[i].height}");
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
}
