using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DeathInfo
{
    public Stage stageType = Stage.Hub;
    public Vector3 deathPosition;
    public int deathStageIndex;
}
public class MapManager : TrackingCameraBase
{
    [Header("Stage Information")]
    public Stage stageType = Stage.Stage1; // Default to Hub stage
    public List<SO_MapInfo> mapInfos = new List<SO_MapInfo>();
    [SerializeField] int currentMapIndex = -1;
    public Vector3 movePoint = Vector3.zero;
    public IStageGimic currentStage;
    
    [SerializeField] SO_MapInfo initMapInfo;

    [Header("WillFragment Information")]
    [SerializeField]  DeathInfo deathInfo = new DeathInfo();
    [SerializeField] GameObject fragmentPrefab;
    public bool getFragment = true;
    GameObject fragmentObject = null;

    public void InitData()
    {
        stageType = Stage.Stage1;
        mapInfos.Clear();
        currentMapIndex = -1;
        movePoint = Vector3.zero;
        mapInfos.Add(initMapInfo);
    }
    void BackToHome()
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
        if(Managers.Game.CurrentState == GameState.Boss)
        {
            Managers.Game.ChangeState(GameState.Home);
            return;
        }
        Managers.Game.ChangeState(GameState.Battle);
    }
    public void StopMove()
    {
        StartCoroutine(TrackingScene());
    }
    public override IEnumerator TrackingScene()
    {
        Init(); // Check Player

        StopPlayer();
        yield return new WaitForSeconds(3f);
        StopPlayerEnd();
    }
    public void TransitionMap(bool isNext = true)
    {
        if (fragmentObject != null)
        {
            Managers.Resource.Destroy(fragmentObject);
            fragmentObject = null;
        }
        // -1 : back to home , maInfos.Count : stage cleared
        if ((isNext && currentMapIndex + 1 >= mapInfos.Count)|| (!isNext && currentMapIndex - 1 < 0))
        {
            if(isNext)
                Managers.Game.playData.stageCleared[(int)stageType] = true;
            BackToHome();
            return;
        }
        // Transition to the next or previous map
        if (isNext)
        {
            currentStage.TransitionMap(++currentMapIndex);
            movePoint = mapInfos[currentMapIndex].entrancePoint;
        }
        else
        {
            currentStage.TransitionMap(--currentMapIndex);
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
