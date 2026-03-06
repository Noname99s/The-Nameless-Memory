using System;
using System.Collections.Generic;
using UnityEngine;

public interface IOptionView
{
    event Action<int> OnResolutionChanged;
    event Action<bool> OnWindowModeChanged;
    event Action OnExitOption;

    void InitResolutionOptions(List<Resolution> resolutions, int currentIndex);
    void SetWindowToggle(bool isFullScreen);
}
