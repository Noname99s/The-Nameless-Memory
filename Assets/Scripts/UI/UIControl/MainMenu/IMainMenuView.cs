using System;

public interface IMainMenuView
{
    event Action OnStartGame;
    event Action OnLoadGame;
    event Action OnOpenOptions;
    event Action OnQuitGame;
}
