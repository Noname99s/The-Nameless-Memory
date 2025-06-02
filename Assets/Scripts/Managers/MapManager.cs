using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public Stage mapListType = Stage.NONE;
    public List<GameObject> mapObjects = new List<GameObject>();
    int currentMapIndex = 0;
    GameObject currentMapObject = null;
    public void EntranceMap()
    {
        if(currentMapObject != null)
        {
            Managers.Resource.Destroy(currentMapObject);
            currentMapObject = null;
        }
        currentMapObject = Managers.Resource.Instantiate(mapObjects[currentMapIndex++]);
    }
}
