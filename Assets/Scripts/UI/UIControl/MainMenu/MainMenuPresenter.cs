public class MainMenuPresenter
{
    IMainMenuView mainView;
    MainMenuModel mainModel;
    public MainMenuPresenter(IMainMenuView view)
    {
        mainView = view;
        mainModel = new MainMenuModel();
        mainView.OnStartGame += HandleStartGame;
        mainView.OnLoadGame += HandleLoadGame;
        mainView.OnOpenOptions += HandleOpenOptions;
        mainView.OnQuitGame += HandleQuitGame;
    }
    void HandleStartGame()
    {
        mainModel.StartGame();
    }
    void HandleLoadGame()
    {
        mainModel.LoadGame();
    }
    void HandleOpenOptions()
    {
        mainModel.OpenOptions();
    }
    void HandleQuitGame()
    {
        mainModel.QuitGame();
    }
    public void Dispose()
    {
        mainView.OnStartGame -= HandleStartGame;
        mainView.OnLoadGame -= HandleLoadGame;
        mainView.OnOpenOptions -= HandleOpenOptions;
        mainView.OnQuitGame -= HandleQuitGame;
    }
}
