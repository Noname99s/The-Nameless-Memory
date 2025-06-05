using UnityEngine;

public class SaveLoadComposition : UIPopup
{
    [SerializeField] private SaveLoadView view;
    private SaveLoadPresenter presenter;
    private void Awake()
    {
        presenter = new SaveLoadPresenter(view);
    }
    private void OnDestroy()
    {
        presenter.Dispose();
    }
}