using System;
using UnityEngine;

public interface IOptionView
{
    event Action<int> OnResolutionChanged;
    event Action<bool> OnWindowModeChanged;

    void SetResolutionOptions(Resolution[] resolutions);
    void SetWindowMode(bool isFullScreen);
}
