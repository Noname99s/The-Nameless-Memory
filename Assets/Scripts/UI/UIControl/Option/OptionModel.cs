using System.Collections.Generic;
using UnityEngine;

public class OptionModel
{
    public List<Resolution> FilteredResolutions { get; private set; }
    
    public OptionModel()
    {
        FilteredResolutions = new List<Resolution>();

        Resolution[] resolutions = Screen.resolutions;
        FilteredResolutions.Clear();
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
                    FilteredResolutions.Add(res);
                }
            }
        }
    }
}
