using UnityEngine;

// Composition Root : Option√¢ ¡∂∏≥
public class OptionComposition : UIPopup
{
    [SerializeField] private OptionView optionView;
    private OptionPresenter optionPresenter;
    private void Awake()
    {
        OptionModel optionModel = new OptionModel();
        optionPresenter = new OptionPresenter(optionView, optionModel);
    }
    private void OnDestroy()
    {
        optionPresenter?.Dispose();
    }
}
