using UnityEngine;

public class MainMenuComposition : MonoBehaviour
{
    [SerializeField] MainMenuView mainMenuView;
    private MainMenuPresenter mainMenuPresenter;
    void Awake()
    {
        mainMenuPresenter = new MainMenuPresenter(mainMenuView);
    }
    private void OnDestroy()
    {
        mainMenuPresenter.Dispose();
    }
}
