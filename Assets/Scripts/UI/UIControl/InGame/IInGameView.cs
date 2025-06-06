using System;
using UnityEngine;
using System.Collections.Generic;

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
    void UpdateDialogAnim(List<string> text);
    void UpdateStaticDialog(List<string> text);
    void UpdateDeathCount(int deathCount);
    void UpdateHP(int hp, int maxHp);
}
