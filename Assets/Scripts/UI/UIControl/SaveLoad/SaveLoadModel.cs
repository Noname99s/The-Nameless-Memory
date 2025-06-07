using System.Collections.Generic;
using Orpul1.UI;
using UnityEngine;

public class SaveLoadModel
{
    public List<PlayData> memoryStackDatas = new List<PlayData>();
    public int currentMemoryStackIndex = 0;
    PlayData currentMemoryData = new PlayData();
    public bool isSaveButtonClicked = false;
    public PlayData CurrentMemoryData
    {
        get 
        {
            return currentMemoryData; 
        }
        set { currentMemoryData = value; }
    }


    public SaveLoadModel()
    {
        memoryStackDatas = Managers.GameData.LoadGameDataCollection();
        while(memoryStackDatas.Count < 3)
        {
            memoryStackDatas.Add(new PlayData()); // Ensure there are always 3 slots
        }
    }

    public void ChooseMemoryStack(int index)
    {
        currentMemoryStackIndex = index;
        currentMemoryData.RecordData(memoryStackDatas[currentMemoryStackIndex]);
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
    }
    public void OpenCurrentPanel(bool isSave)
    {
        Managers.Sound.PlayUISound((int)EUiSound.Popup);
        isSaveButtonClicked = isSave;
    }
    public void HandleYesButtonClicked()
    {
        Managers.Sound.PlayUISound((int)EUiSound.Confirm);
        if (isSaveButtonClicked)
        {
            SaveCurrentMemoryData();
        }
        else
        {
            LoadCurrentMemoryData();
        }
    }
    void SaveCurrentMemoryData()
    {
        currentMemoryData.RecordData(Managers.Game.playData);
        memoryStackDatas[currentMemoryStackIndex].RecordData(Managers.Game.playData);
        Managers.GameData.SaveGameDataCollection(memoryStackDatas);
    }
    void LoadCurrentMemoryData()
    {
        Managers.Game.playData.RecordData(currentMemoryData);
        Time.timeScale = 1f; // Resume game time
        Managers.Game.ChangeState(GameState.Home);
    }
    public void ExitSave()
    {
        Managers.Sound.PlayUISound((int)EUiSound.Cancel);
        Managers.UI.ClosePopupUI<SaveLoadComposition>();
    }

}