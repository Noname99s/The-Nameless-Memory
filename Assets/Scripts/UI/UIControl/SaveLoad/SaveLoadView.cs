using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Orpul1.UI;

public class SaveLoadView : MonoBehaviour , ISaveLoadView
{
    enum TextType
    {
        Title,
        PlayTime,
        StageClear,
        DeathCount,
        Max,
    }
    [SerializeField] Button[] memoryStack = new Button[3];
    [SerializeField] Button saveButton;
    [SerializeField] Button loadButton;
    [SerializeField] Button exitButton;
    [SerializeField] TextMeshProUGUI[] dataText = new TextMeshProUGUI[(int)TextType.Max];

    [Header("Current Data")]
    [SerializeField] GameObject currentDataPanel;
    [SerializeField] TextMeshProUGUI[] currentDataText = new TextMeshProUGUI[(int)TextType.Max];
    [SerializeField] Button yesSave;
    [SerializeField] Button noSave;

    public event Action<int> OnMemoryStackButtonClicked;
    public event Action OnYesButtonClicked;
    public event Action OnSaveButtonClicked;
    public event Action OnLoadButtonClicked;
    public event Action OnExitButtonClicked;

    void Start()
    {
        for (int i = 0; i < memoryStack.Length; i++)
        {
            //람다식에서 i를 직접 참조하면, 클릭 이벤트가 발생할 때 이미 for문이 끝나서 i 값이 마지막 값(memoryStack.Length)으로 고정됨
            int index = i;
            memoryStack[i].onClick.AddListener(() => OnMemoryStackButtonClicked(index));
        }
        yesSave.onClick.AddListener
            (() => 
            { 
                currentDataPanel.SetActive(false);
                OnYesButtonClicked?.Invoke();
            });
        noSave.onClick.AddListener
           (() =>
            {
                Managers.Sound.PlayUISound((int)EUiSound.Cancel);
                currentDataPanel.SetActive(false);
            });
        saveButton.onClick.AddListener(() => OnSaveButtonClicked?.Invoke());
        loadButton.onClick.AddListener(() => OnLoadButtonClicked?.Invoke());
        exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
    }
    void OnDestroy()
    {
        for (int i = 0; i < memoryStack.Length; i++)
        {
            memoryStack[i].onClick.RemoveAllListeners();
        }
        saveButton.onClick.RemoveAllListeners();
        loadButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        yesSave.onClick.RemoveAllListeners();
        noSave.onClick.RemoveAllListeners();
    }
    public void GetPlayerData(int index, PlayData data)
    {
        dataText[(int)TextType.Title].text = "Memory " + (index + 1);
        if (data == null)
        {
            for (int i = 0; i < currentDataText.Length; i++)
            {
                currentDataText[i].text = "No Data";
            }
        }
        else
        {
            int lastStageClearIndex = -1;
            for (int i = 0; i < (int)Stage.Max; i++)
            {
                if (data.stageCleared[i])
                    lastStageClearIndex = i;
            }
            Stage lastStageClear = (Stage)lastStageClearIndex;
            TimeSpan timeSpan = TimeSpan.FromSeconds(data.playTime);
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            dataText[(int)TextType.PlayTime].text = formattedTime;
            dataText[(int)TextType.StageClear].text = lastStageClear.ToString() + " Clear";
            dataText[(int)TextType.DeathCount].text = data.playerStatus.deathCount.ToString();
        }
    }
    public void GetCurrentData(int index,bool isSave, PlayData data)
    {
        int lastStageClearIndex = -1;
        for (int i = 0; i < (int)Stage.Max; i++)
        {
            if (data.stageCleared[i])
                lastStageClearIndex = i;
        }
        Stage lastStageClear = (Stage)lastStageClearIndex;
        TimeSpan timeSpan = TimeSpan.FromSeconds(data.playTime);
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        string saveText = isSave ? "에 현재 데이터를 저장하시" : "를 현재데이터에 불러오시";
        currentDataText[(int)TextType.Title].text = "Memory " + (index + 1) + saveText + "겠습니까?";
        currentDataText[(int)TextType.PlayTime].text = formattedTime;
        currentDataText[(int)TextType.StageClear].text = lastStageClear.ToString() + " Clear";
        currentDataText[(int)TextType.DeathCount].text = data.playerStatus.deathCount.ToString();
        currentDataPanel.SetActive(true);
    }
}