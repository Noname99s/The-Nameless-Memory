using System.Collections.Generic;
using UnityEngine;

public class DataSaveLoadTest : MonoBehaviour
{
    GameDataManager gameDataManager = new GameDataManager();
    public List<PlayData> allPlayerSaves = new List<PlayData>();


    void Start()
    {
        Debug.Log("SavePath: " + gameDataManager.GetFilePath());
        allPlayerSaves = gameDataManager.LoadGameDataCollection();
        if (allPlayerSaves.Count == 0)
        { 
            allPlayerSaves.Add(new PlayData());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            allPlayerSaves.Clear();
            for (int i = 0; i < 10; i++)
            {
                bool[] stageCleared = new bool[(int)Stage.Max];
                int[] stage_Will_fragmentCount = new int[(int)Stage.Max];
                for (int j = 0; j < (int)Stage.Max; j++)
                {
                    stageCleared[j] = false; // 기본값 설정
                    stage_Will_fragmentCount[j] = j; // 기본값 설정
                }
                allPlayerSaves.Add(new PlayData(i, 2*i, 3*i, stageCleared, stage_Will_fragmentCount));
            }
            gameDataManager.SaveGameDataCollection(allPlayerSaves);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            allPlayerSaves = gameDataManager.LoadGameDataCollection();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            allPlayerSaves.Clear();
            for(int i = 0; i < 10; i++)
            {
                allPlayerSaves.Add(new PlayData()); // 초기화 후 기본 데이터 추가
            }
            gameDataManager.SaveGameDataCollection(allPlayerSaves);
        }
    }
}
