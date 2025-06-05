using System;
using UnityEngine;

public interface IInGameView
{
    event Action OnPauseGame;
    event Action OnResumeGame;
    event Action OnOpenSaveLoad;
    event Action OnOpenOptions;
    event Action OnBackToMainMenu;
    event Action OnQuitGame;


    //Player
    void UpdateDialogAnim(string text);
    void UpdateDeathCount(int deathCount);
    void UpdateHP(int hp, int maxHp);
}
