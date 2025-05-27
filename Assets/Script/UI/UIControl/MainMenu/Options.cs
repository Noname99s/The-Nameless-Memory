using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle windowToggle;


    Resolution[] resolutions;
    List<Resolution> filteredResolutions = new List<Resolution>();
    void Start()
    {
        resolutions = Screen.resolutions;

        filteredResolutions.Clear();
        foreach (var res in resolutions)
        {
            float aspectRatio = (float)res.width / res.height;
            if (Mathf.Abs(aspectRatio - (16f / 9f)) < 0.01f) // 16:9 비율 체크
            {
                filteredResolutions.Add(res);
            }
        }

        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            options.Add(option);

            if (filteredResolutions[i].width == Screen.currentResolution.width &&
               filteredResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i; // 현재 해상도와 일치하는 옵션 선택
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        windowToggle.onValueChanged.AddListener(SetWindowMode);
    }

    void SetResolution(int index)
    {
        Resolution selectedResolution = filteredResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
    void SetWindowMode(bool isFullScreen)
    {
        Screen.fullScreen = !isFullScreen;
    }
}
