using System;
using UnityEngine;

public interface ISaveLoadView
{
    event Action<int> OnMemoryStackButtonClicked;
    event Action OnYesButtonClicked;
    event Action OnSaveButtonClicked;
    event Action OnLoadButtonClicked;
    event Action OnExitButtonClicked;

    void GetPlayerData(int index, PlayData data);
    void GetCurrentData(int index, bool isSave, PlayData data);
}