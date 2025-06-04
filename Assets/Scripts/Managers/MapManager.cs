using UnityEngine;
using System.Collections.Generic;
using System;
[Serializable]
public class DeathInfo
{
    public Stage stageType;
    public Vector3 deathPosition;
    public int deathStageIndex;
}
public class MapManager : MonoBehaviour
{
    public Stage stageType = Stage.Hub; // Default to Hub stage
    public List<SO_MapInfo> mapInfos = new List<SO_MapInfo>();
    [SerializeField] int currentMapIndex = -1;
    GameObject currentMapObject = null;
    public Vector3 movePoint = Vector3.zero;
    [SerializeField] DeathInfo deathInfo = new DeathInfo();
    public void EntranceMap(bool isNext = true)
    {
        if (currentMapObject != null)
        {
            Managers.Resource.Destroy(currentMapObject);
            currentMapObject = null;
        }
        if((isNext && currentMapIndex + 1 >= mapInfos.Count)|| (!isNext && currentMapIndex - 1 < 0))
        {
            if(isNext)
                Managers.Game.playData.stageCleared[(int)stageType] = true;
            currentMapIndex = -1;
            Managers.Game.ChangeState(GameState.Home);
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
