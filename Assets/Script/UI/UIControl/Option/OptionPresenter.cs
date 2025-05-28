using UnityEngine;

public class OptionPresenter
{
    IOptionView optionView;
    OptionModel optionModel;

    public OptionPresenter(IOptionView view)
    {
        optionModel = new OptionModel();
        optionView = view;
        optionView.InitResolutionOptions(optionModel.filteredResolutions.ToArray());
        optionView.SetWindowToggle(Screen.fullScreen);
        optionView.OnResolutionChanged += HandleResolutionChanged;
        optionView.OnWindowModeChanged += HandleWindowModeChanged;
    }
    void HandleResolutionChanged(int index)
    {
        optionModel.SetResolution(index);
    }
    void HandleWindowModeChanged(bool isFullScreen)
    {
        optionModel.SetWindowMode(isFullScreen);
    }

    public void Dispose()
    {
        optionView.OnResolutionChanged -= HandleResolutionChanged;
        optionView.OnWindowModeChanged -= HandleWindowModeChanged;
    }
}
