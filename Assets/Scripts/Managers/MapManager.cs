using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public Stage stageType = Stage.Hub; // Default to Hub stage
    public List<SO_MapInfo> mapInfos = new List<SO_MapInfo>();
    [SerializeField] int currentMapIndex = -1;
    GameObject currentMapObject = null;
    public Vector3 movePoint = Vector3.zero;
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
                Managers.Game.stageCleared[(int)stageType] = true;
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
}
