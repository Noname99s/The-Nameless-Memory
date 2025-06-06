using UnityEngine;
using System.Collections.Generic;
using System;
[Serializable]
public class DeathInfo
{
    public Stage stageType = Stage.Hub;
    public Vector3 deathPosition;
    public int deathStageIndex;
}
public class MapManager : MonoBehaviour
{
    public List<bool>[] dialogComlete = new List<bool>[(int)Stage.Max]
    {
        new List<bool>(), // Stage1
        new List<bool>(), // Stage2
        new List<bool>(), // Stage3
        new List<bool>(), // Stage4
        new List<bool>(), // BossStage
    };
    [Header("Stage Information")]
    public Stage stageType = Stage.Hub; // Default to Hub stage
    public List<SO_MapInfo> mapInfos = new List<SO_MapInfo>();
    [SerializeField] int currentMapIndex = -1;
    public Vector3 movePoint = Vector3.zero;
    GameObject currentMapObject = null;

    [Header("WillFragment Information")]
    [SerializeField] DeathInfo deathInfo = new DeathInfo();
    [SerializeField] GameObject fragmentPrefab;
    public bool getFragment = false;
    GameObject fragmentObject = null;

    public void StartStage(Stage stage , int interactionCount)
    {
        while(dialogComlete[(int)stage].Count < interactionCount)
        {
            dialogComlete[(int)stage].Add(false);
        }
    }
    public void BackToHome()
    {
        currentMapIndex = -1;
        movePoint = Vector3.zero;
        Managers.Game.ChangeState(GameState.Home);
    }
    public void RespawnPlayer()
    {
        currentMapIndex--;
        if (currentMapIndex < -1)
            currentMapIndex = -1;
        Managers.Game.ChangeState(GameState.Battle);
    }
    public void TransitionMap(bool isNext = true)
    {
        if (currentMapObject != null)
        {
            Managers.Resource.Destroy(currentMapObject);
            currentMapObject = null;
        }
        if (fragmentObject != null)
        {
            Managers.Resource.Destroy(fragmentObject);
            fragmentObject = null;
        }
        if ((isNext && currentMapIndex + 1 >= mapInfos.Count)|| (!isNext && currentMapIndex - 1 < 0))
        {
            if(isNext)
                Managers.Game.playData.stageCleared[(int)stageType] = true;
            BackToHome();
            return;
        }
        if(isNext)
        {
            currentMapObject = Managers.Resource.Instantiate(mapInfos[++currentMapIndex].mapObject);
            movePoint = mapInfos[currentMapIndex].entrancePoint;
        }
        else
        {
            currentMapObject = Managers.Resource.Instantiate(mapInfos[--currentMapIndex].mapObject);
            movePoint = mapInfos[currentMapIndex].exitPoint;
        }
        // Set Fragment Object
        if (!getFragment && stageType == deathInfo.stageType && currentMapIndex == deathInfo.deathStageIndex)
        {
            Vector2 spawnPos = deathInfo.deathPosition;
            spawnPos.y += 2f;
            fragmentObject = Managers.Resource.Instantiate(fragmentPrefab, spawnPos, Quaternion.identity);
        }

        Managers.Player.ChangePlayerPos(movePoint);
    }
    public void RecordDeathInfo(Vector2 deathPoint)
    {
        deathInfo.deathPosition = deathPoint;
        deathInfo.stageType = stageType;
        deathInfo.deathStageIndex = currentMapIndex;
        Debug.Log("Death recorded at: " + deathPoint + " in stage: " + stageType + " at index: " + currentMapIndex);
    }
}
