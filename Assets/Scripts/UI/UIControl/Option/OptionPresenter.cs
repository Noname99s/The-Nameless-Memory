using Orpul1.UI;
using System.Collections.Generic;
using UnityEngine;

public class OptionPresenter
{
    readonly IOptionView optionView;
    readonly OptionModel optionModel;

    public OptionPresenter(IOptionView view , OptionModel model)
    {
        optionModel = model;
        optionView = view;

        InitializeResolutionOptions();
        optionView.SetWindowToggle(Screen.fullScreen);
        optionView.OnResolutionChanged += HandleResolutionChanged;
        optionView.OnWindowModeChanged += HandleWindowModeChanged;
        optionView.OnExitOption += HandleExitOption;
    }
    void InitializeResolutionOptions()
    {
        List<Resolution> resolutions  = optionModel.FilteredResolutions;
        List<string> options = new List<string>(resolutions.Count);
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            options.Add($"{resolutions[i].width}x{resolutions[i].height}@{resolutions[i].refreshRateRatio.value}Hz");
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentIndex = i;
            }
        }
        optionView.InitResolutionOptions(options , currentIndex);
    }
    void HandleResolutionChanged(int index)
    {
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
        Resolution selectedResolution = optionModel.FilteredResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
    void HandleWindowModeChanged(bool isFullScreen)
    {
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
        Screen.fullScreen = !isFullScreen;
    }
    void HandleExitOption()
    {
        Managers.Sound.PlayUISound((int)EUiSound.Cancel);
        Managers.UI.ClosePopupUI<OptionComposition>();
    }

    public void Dispose()
    {
        optionView.OnResolutionChanged -= HandleResolutionChanged;
        optionView.OnWindowModeChanged -= HandleWindowModeChanged;
        optionView.OnExitOption -= HandleExitOption;
    }
}
