using System.Collections.Generic;
using UnityEngine;

public class OptionModel
{
    public List<Resolution> filteredResolutions = new List<Resolution>();
    public OptionModel()
    {
        Resolution[] resolutions = Screen.resolutions;
        filteredResolutions.Clear();
        foreach (var res in resolutions)
        {
            float aspectRatio = (float)res.width / res.height;
            if (Mathf.Abs(aspectRatio - (16f / 9f)) < 0.01f) // 16:9 비율 체크
            {
                filteredResolutions.Add(res);
            }
        }
    }
    public void SetResolution(int index)
    {
        Resolution selectedResolution = filteredResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
    public void SetWindowMode(bool isFullScreen)
    {
        Screen.fullScreen = !isFullScreen;
    }

    public void ExitOption()
    {
        Managers.UI.ClosePopupUI<OptionComposition>();
    }
}
