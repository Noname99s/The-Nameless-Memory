using UnityEngine;

// Composition Root : Option√¢ ¡∂∏≥
public class OptionComposition : MonoBehaviour
{
    [SerializeField] private OptionView optionView;
    private OptionPresenter optionPresenter;
    private void Awake()
    {
        optionPresenter = new OptionPresenter(optionView);
    }
    private void OnDestroy()
    {
        optionPresenter.Dispose();
    }
}
