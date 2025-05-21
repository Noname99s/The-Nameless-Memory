using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomTilemapEditor : EditorWindow
{
    enum LayerType
    {
        None,
        BackGround,
        Floor,
        Interact,
        Max
    }

    [MenuItem("Tilemap Custom/Tilemap Settings")]
    public static void ShowWindow()
    {
        GetWindow<CustomTilemapEditor>("Tilemap Edit");
    }

    Tilemap floorTile;
    Tilemap interactTile;
    Tilemap backGroundTile;

    int guiSpace = 20;
    int mapLevel = 0;
    int[] toggleSwitch = new int[(int)LayerType.Max];
    string newFloorTileName = string.Empty;
    string newInteractTileName = string.Empty;
    string newBGTileName = string.Empty;
    Transform floorParent;
    Transform backGroundParent;

    private void OnEnable()
    {
        // Tile Palette 열고 CustomEditor와 붙여주기
        EditorApplication.ExecuteMenuItem("Window/2D/Tile Palette");

        //Tilemap 찾기
        floorTile = GameObject.FindGameObjectWithTag("Floor")?.GetComponent<Tilemap>();
        if (floorTile != null)
            floorParent = floorTile.transform.parent;
        backGroundTile = GameObject.FindGameObjectWithTag("BackGround")?.GetComponent<Tilemap>();
        if (backGroundTile != null)
            backGroundParent = backGroundTile.transform.parent;
        interactTile = GameObject.FindGameObjectWithTag("Interact")?.GetComponent<Tilemap>();
    }
    private void OnGUI()
    {
        GUILayout.Label("Tilemap Settings", EditorStyles.boldLabel);
        // 레벨 선택 -- 레벨별 파일 로드 + 룰타일 로드
        mapLevel = EditorGUILayout.Popup("Map Level", mapLevel, new string[] { "Map1", "Map2", "Map3" });
        if (GUILayout.Button("Reset Scene", GUILayout.Width(100f), GUILayout.Height(20f)))
        {
            if (EditorUtility.DisplayDialog("Reset Scene", "Are you sure you want to reset the scene?", "Yes", "No"))
            {
                DestroyImmediate(floorParent.gameObject);
                DestroyImmediate(backGroundParent.gameObject);
                List<GameObject> tilemaps = new List<GameObject>();
                tilemaps.AddRange(GameObject.FindGameObjectsWithTag("Interact"));
                foreach (GameObject tilemap in tilemaps)
                {
                    DestroyImmediate(tilemap.transform.parent.gameObject);
                }
            }
        }
        EditorGUILayout.Space(guiSpace);

        guiSpace = EditorGUILayout.IntSlider("Space", guiSpace, 0, 30);
        #region Floor Options
        toggleSwitch[(int)LayerType.Floor] = EditorGUILayout.ToggleLeft("Floor", toggleSwitch[(int)LayerType.Floor] == 1) ? 1 : 0;

        if (EditorGUILayout.BeginFadeGroup(toggleSwitch[(int)LayerType.Floor]))
        {
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Create", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                if (floorParent == null)
                    floorTile = CreateArea(LayerType.Floor).GetComponent<Tilemap>();
                else
                    floorTile = CreateTilemap(LayerType.Floor, null, newFloorTileName, floorParent).GetComponent<Tilemap>();
                newFloorTileName = string.Empty;
            }
            newFloorTileName = EditorGUILayout.TextField("New Floor Name", newFloorTileName);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Load", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                Tilemap loadTile = LoadPrefab(LayerType.Floor , floorParent == null);
                if (loadTile != null)
                    floorTile = loadTile;
                if (floorParent != null)
                {
                    floorTile.transform.parent = floorParent;
                }
            }
            if (floorTile != null)
            {
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(floorTile);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(floorTile);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) floorTile.ClearAllTiles();
                if (GUILayout.Button("Destroy", GUILayout.Width(60f), GUILayout.Height(15f)))
                {
                    DestroyImmediate(floorTile.gameObject);
                    floorTile = GameObject.FindGameObjectWithTag("Floor")?.GetComponent<Tilemap>();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            floorTile = (Tilemap)EditorGUILayout.ObjectField("Basemap", floorTile, typeof(Tilemap), true);
            if (floorTile != null)
                floorTile.GetComponent<TilemapRenderer>().sortingOrder = EditorGUILayout.IntField("Sorting Order", floorTile.GetComponent<TilemapRenderer>().sortingOrder);
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndFadeGroup();
        #endregion
        EditorGUILayout.Space(guiSpace);
        
        #region Interact Options
        toggleSwitch[(int)LayerType.Interact] = EditorGUILayout.ToggleLeft("Interact", toggleSwitch[(int)LayerType.Interact] == 1) ? 1 : 0;
        if (EditorGUILayout.BeginFadeGroup(toggleSwitch[(int)LayerType.Interact]))
        {
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Create", GUILayout.Width(50f), GUILayout.Height(20f)))
            {
                interactTile = CreateArea(LayerType.Interact, null, newInteractTileName).GetComponent<Tilemap>();
                newInteractTileName = string.Empty;
            }
            newInteractTileName = EditorGUILayout.TextField("New Platform Name", newInteractTileName);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Load", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                Tilemap loadTile = LoadPrefab(LayerType.Interact);
                if (loadTile != null)
                    interactTile = loadTile;
            }
            if (interactTile != null)
            {
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(interactTile);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(interactTile);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) interactTile.ClearAllTiles();
                if (GUILayout.Button("Destroy", GUILayout.Width(60f), GUILayout.Height(15f)))
                {
                    DestroyImmediate(interactTile.transform.parent.gameObject);
                    interactTile = GameObject.FindGameObjectWithTag("Interact")?.GetComponent<Tilemap>();
                }
            }
            GUILayout.EndHorizontal();
            interactTile = (Tilemap)EditorGUILayout.ObjectField("Interact Tilemap", interactTile, typeof(Tilemap), true);
        }
        EditorGUILayout.EndFadeGroup();
        #endregion
        EditorGUILayout.Space(guiSpace);

        #region BackGround Options
        toggleSwitch[(int)LayerType.BackGround] = EditorGUILayout.ToggleLeft("BackGround", toggleSwitch[(int)LayerType.BackGround] == 1) ? 1 : 0;
        if (EditorGUILayout.BeginFadeGroup(toggleSwitch[(int)LayerType.BackGround]))
        {
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Create", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                if (backGroundParent == null) 
                    backGroundTile = CreateArea(LayerType.BackGround , null , newBGTileName).GetComponent<Tilemap>();
                else
                    backGroundTile = CreateTilemap(LayerType.BackGround, null, newBGTileName, backGroundParent).GetComponent<Tilemap>();
                newBGTileName = string.Empty;
            }
            newBGTileName = EditorGUILayout.TextField("New BackGround Name", newBGTileName);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Load", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                Tilemap loadTile = LoadPrefab(LayerType.BackGround, backGroundParent == null);
                if (loadTile != null)
                    backGroundTile = loadTile;
                if (backGroundParent != null)
                {
                    backGroundTile.transform.parent = backGroundParent;
                }
            }
            else
            if(backGroundTile != null)
            {
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(backGroundTile);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(backGroundTile);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) backGroundTile.ClearAllTiles();
                if (GUILayout.Button("Destroy", GUILayout.Width(60f), GUILayout.Height(15f)))
                {
                    DestroyImmediate(backGroundTile.gameObject);
                    backGroundTile = GameObject.FindGameObjectWithTag("BackGround")?.GetComponent<Tilemap>();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            backGroundTile = (Tilemap)EditorGUILayout.ObjectField("BackGround Tilemap", backGroundTile, typeof(Tilemap), true);
            if(backGroundTile != null)
                backGroundTile.GetComponent<TilemapRenderer>().sortingOrder 
                    = EditorGUILayout.IntField("Sorting Order", backGroundTile.GetComponent<TilemapRenderer>().sortingOrder);
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndFadeGroup();
        #endregion

    }

    private GameObject CreateArea(LayerType layerType = LayerType.None, GameObject tilemapPrefab = null, string tileMapName = null)
    {
        // 부모 오브젝트 생성
        GameObject parent = new GameObject("Tilemap");
        parent.transform.position = Vector3.zero;
        parent.transform.rotation = Quaternion.identity;
        parent.transform.localScale = Vector3.one;
        parent.AddComponent<Grid>();
        if (layerType != LayerType.BackGround)
        {
            parent.AddComponent<Rigidbody2D>();
            parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            parent.AddComponent<CompositeCollider2D>();
        }
        if (layerType == LayerType.Floor)
            floorParent = parent.transform;
        else if (layerType == LayerType.BackGround)
            backGroundParent = parent.transform;

        // 자식 오브젝트 생성
        GameObject tilemapObject = CreateTilemap(layerType, tilemapPrefab, tileMapName, parent.transform);
        return tilemapObject;
    }
    private GameObject CreateTilemap(LayerType layerType = LayerType.None, GameObject tilemapPrefab = null, string tileMapName = null, Transform parent = null)
    {
        GameObject tilemapObject;
        if (tilemapPrefab == null)
        {
            tilemapObject = new GameObject(tileMapName != null && tileMapName != "" ? tileMapName : layerType.ToString());
            tilemapObject.AddComponent<Tilemap>();
            tilemapObject.AddComponent<TilemapRenderer>();
            if (layerType != LayerType.None)
            {
                tilemapObject.tag = layerType.ToString();
                tilemapObject.GetComponent<TilemapRenderer>().sortingLayerName = layerType.ToString();
                if (layerType != LayerType.BackGround)
                {
                    tilemapObject.AddComponent<TilemapCollider2D>();
                    tilemapObject.GetComponent<TilemapCollider2D>().compositeOperation = Collider2D.CompositeOperation.Merge;
                }
            }
        }
        else
            tilemapObject = Instantiate(tilemapPrefab);
        tilemapObject.transform.position = Vector3.zero;
        tilemapObject.transform.rotation = Quaternion.identity;
        tilemapObject.transform.localScale = Vector3.one;
        if (parent != null) 
            tilemapObject.transform.parent = parent.transform;
        return tilemapObject;
    }
    #region Save & Load
    private void SavePrefab(Tilemap saveTile)
    {
        if (saveTile == null)
        {
            Debug.LogError("Tilemap is not assigned.");
            return;
        }
        string prefabPath = EditorUtility.SaveFilePanel(
        "Save Tilemap Data",
        "Assets/CustomTileMapEditor/MapData/Map" + (mapLevel + 1),
        "tilemap_data.prefab",
        "prefab"
         );
        if (string.IsNullOrEmpty(prefabPath))
        {
            Debug.Log("Save operation cancelled.");
            return;
        }
        PrefabUtility.SaveAsPrefabAsset(saveTile.gameObject, prefabPath);
        Debug.Log("Prefab saved at: " + prefabPath);
    }
    private Tilemap LoadPrefab(LayerType layerType ,bool isCreateArea = true)
    {
        string prefabPath = EditorUtility.OpenFilePanel(
        "Load Tilemap Data",
        "Assets/CustomTileMapEditor/MapData/Map" + (mapLevel + 1),
        "prefab"
        );
        if (!string.IsNullOrEmpty(prefabPath))
        {
            GameObject prefab = PrefabUtility.LoadPrefabContents(prefabPath);
            string prefabName = Path.GetFileNameWithoutExtension(prefabPath);
            Tilemap loadTile;
            if (isCreateArea)
                loadTile = CreateArea(layerType, prefab, prefabName).GetComponent<Tilemap>();
            else
                loadTile = CreateTilemap(layerType, prefab, prefabName).GetComponent<Tilemap>();
            return loadTile;
        }
        else
        {
            Debug.Log("Load operation cancelled.");
            return null;
        }
    }

    private void Paint(Tilemap paintMap)
    {
        string prefabPath = EditorUtility.OpenFilePanel(
        "Paint Tilemap Data",
        "Assets/CustomTileMapEditor/MapData/Map" + (mapLevel + 1),
        "prefab"
        );
        if (!string.IsNullOrEmpty(prefabPath))
        {
            paintMap.ClearAllTiles();
            GameObject prefab = PrefabUtility.LoadPrefabContents(prefabPath);
            BoundsInt bounds = prefab.GetComponent<Tilemap>().cellBounds;
            for (int x = bounds.x; x < bounds.xMax; x++)
            {
                for (int y = bounds.y; y < bounds.yMax; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    TileBase tile = prefab.GetComponent<Tilemap>().GetTile(position);
                    if (tile != null)
                    {
                        paintMap.SetTile(position, tile);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Paint operation cancelled.");
        }
    }
    #endregion
}

