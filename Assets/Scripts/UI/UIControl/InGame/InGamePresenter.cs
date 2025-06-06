using Orpul1.Player;
using UnityEngine;

public class InGamePresenter
{
    IInGameView inGameView;
    InGameModel inGameModel;

    public InGamePresenter(IInGameView view)
    {
        inGameView = view;
        inGameModel = new InGameModel();
        inGameView.OnPauseGame += HandlePauseGame;
        inGameView.OnResumeGame += HandleResumeGame;
        inGameView.OnOpenSaveLoad += HandleOpenSaveLoad;
        inGameView.OnOpenOptions += HandleOpenOptions;
        inGameView.OnBackToMainMenu += HandleBackToMainMenu;
        inGameView.OnQuitGame += HandleQuitGame;
    }

    void HandlePauseGame()
    {
        inGameModel.PauseGame();
    }
    void HandleResumeGame()
    {
        inGameModel.ResumeGame();
    }
    void HandleOpenSaveLoad()
    {
        inGameModel.OpenSaveLoad();
    }
    void HandleOpenOptions()
    {
        inGameModel.OpenOptions();
    }
    void HandleBackToMainMenu()
    {
        inGameModel.BackToMainMenu();
    }
    void HandleQuitGame()
    {
        inGameModel.QuitGame();
    }
    public void Dispose()
    {
        inGameView.OnPauseGame -= HandlePauseGame;
        inGameView.OnResumeGame -= HandleResumeGame;
        inGameView.OnOpenOptions -= HandleOpenOptions;
        inGameView.OnBackToMainMenu -= HandleBackToMainMenu;
        inGameView.OnQuitGame -= HandleQuitGame;
    }
}
