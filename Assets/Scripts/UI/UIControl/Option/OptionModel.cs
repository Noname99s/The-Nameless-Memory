using System.Collections.Generic;
using Orpul1.UI;
using UnityEngine;

public class OptionModel
{
    public List<Resolution> filteredResolutions = new List<Resolution>();
    public OptionModel()
    {
        Resolution[] resolutions = Screen.resolutions;
        filteredResolutions.Clear();
        foreach (Resolution res in resolutions)
        {
            if (res.width < 1280 || res.height < 720) // 1280x720 이하 해상도 제외
                continue;
            float aspectRatio = (float)res.width / res.height;

            if (Mathf.Abs(aspectRatio - (16f / 9f)) < 0.01f) // 16:9 비율 체크
            {
                // 60hz , 120hz만 필터링 ( 0.01f 오차 범위 hz 체크)
                if(res.refreshRateRatio.value >=60f && res.refreshRateRatio.value % 60 == 0)
                {
                    //if (!filteredResolutions.Exists(r => r.width == res.width && r.height == res.height))
                    filteredResolutions.Add(res);
                }
            }
        }
    }
    public void SetResolution(int index)
    {
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
        Resolution selectedResolution = filteredResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
    public void SetWindowMode(bool isFullScreen)
    {
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
        Screen.fullScreen = !isFullScreen;
    }

    public void ExitOption()
    {
        Managers.Sound.PlayUISound((int)EUiSound.Cancel);
        Managers.UI.ClosePopupUI<OptionComposition>();
    }
}
