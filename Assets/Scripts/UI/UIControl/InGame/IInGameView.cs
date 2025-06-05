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

    event Action OnEndStaticDialog;


    //Player
    void UpdateDialogAnim(string text);
    void UpdateStaticDialog(string text);
    void UpdateDeathCount(int deathCount);
    void UpdateHP(int hp, int maxHp);
}
