using System;
using UnityEngine;

public interface IOptionView
{
    event Action<int> OnResolutionChanged;
    event Action<bool> OnWindowModeChanged;

    void InitResolutionOptions(Resolution[] resolutions);
    void SetWindowToggle(bool isFullScreen);
}
