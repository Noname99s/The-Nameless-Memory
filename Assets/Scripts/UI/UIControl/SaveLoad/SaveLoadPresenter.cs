using UnityEngine;

public class SaveLoadPresenter
{
    ISaveLoadView view;
    SaveLoadModel model;

    public SaveLoadPresenter(ISaveLoadView view)
    {
        model = new SaveLoadModel();
        this.view = view;
        this.view.OnMemoryStackButtonClicked += HandleMemoryStackSelected;
        this.view.OnYesButtonClicked += HandleYesButtonClicked;
        this.view.OnSaveButtonClicked += HandleSaveButtonClicked;
        this.view.OnLoadButtonClicked += HandleLoadButtonClicked;
        this.view.OnExitButtonClicked += HandleExitButtonClicked;
        HandleMemoryStackSelected(0); 
    }
    void HandleMemoryStackSelected(int index)
    {
        model.ChooseMemoryStack(index);
        PlayData data = model.CurrentMemoryData;
        view.GetPlayerData(index, data);
    }
    void HandleYesButtonClicked()
    {
        model.HandleYesButtonClicked();
    }
    void HandleSaveButtonClicked()
    {
        model.OpenCurrentPanel(true);
        view.GetCurrentData(model.currentMemoryStackIndex, true, Managers.Game.playData);
    }
    void HandleLoadButtonClicked()
    {
        model.OpenCurrentPanel(false);
        view.GetCurrentData(model.currentMemoryStackIndex, false, model.CurrentMemoryData);
    }
    void HandleExitButtonClicked()
    {
        model.ExitSave();
    }
    public void RecordData(PlayData data)
    {
        model.CurrentMemoryData.RecordData(data);
    }
    public void Dispose()
    {
        view.OnMemoryStackButtonClicked -= HandleMemoryStackSelected;
        view.OnYesButtonClicked -= HandleYesButtonClicked;
        view.OnLoadButtonClicked -= HandleLoadButtonClicked;
        view.OnExitButtonClicked -= HandleExitButtonClicked;
    }
}