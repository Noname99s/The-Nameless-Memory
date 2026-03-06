using Orpul1.UI;
using UnityEngine;

public class OptionPresenter
{
    readonly IOptionView optionView;
    readonly OptionModel optionModel;

    public OptionPresenter(IOptionView view , OptionModel model)
    {
        optionModel = model;
        optionView = view;

        optionView.InitResolutionOptions(optionModel.filteredResolutions , optionModel.currentResolutionIndex);
        optionView.SetWindowToggle(Screen.fullScreen);
        optionView.OnResolutionChanged += HandleResolutionChanged;
        optionView.OnWindowModeChanged += HandleWindowModeChanged;
        optionView.OnExitOption += HandleExitOption;
    }
    void HandleResolutionChanged(int index)
    {
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
        Resolution selectedResolution = optionModel.filteredResolutions[index];
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
