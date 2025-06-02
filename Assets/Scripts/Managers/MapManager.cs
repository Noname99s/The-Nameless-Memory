using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public Vector3 playerSpawnPoint;
    public Stage mapListType = Stage.Hub;
    public List<GameObject> mapObjects = new List<GameObject>();
    [SerializeField] int currentMapIndex = 0;
    GameObject currentMapObject = null;
    public void EntranceMap(bool isNext = true)
    {
        if (currentMapObject != null)
        {
            Managers.Resource.Destroy(currentMapObject);
            currentMapObject = null;
        }
        if (currentMapIndex >= mapObjects.Count || (!isNext && currentMapIndex - 1 <= 0))
        {
            if(!isNext) currentMapIndex--;
            if(currentMapIndex >= mapObjects.Count)
            {
                Managers.Game.stageCleared[(int)mapListType] = true;
                currentMapIndex = 0;
            }
            Managers.Game.ChangeState(GameState.Home);
            return;
        }
        currentMapObject = Managers.Resource.Instantiate(mapObjects[currentMapIndex]);
        if(isNext) currentMapIndex++;
        else currentMapIndex--;
    }
}
