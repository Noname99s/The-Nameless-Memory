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

    string[] mapFolder = new string[] { "Hub", "Map1", "Map2", "Map3", "Map4", "Map5" };
    int[] toggleSwitch = new int[(int)LayerType.Max];
    string newFloorTileName = string.Empty;
    string newInteractTileName = string.Empty;
    string newBGTileName = string.Empty;
    Transform floorParent;
    Transform backGroundParent;
    Transform interactParent;
    Transform rootParent;
    bool floorIsTrigger = false;
    bool interactIsTrigger = false;
    bool dummyTrigger = false;

    private void OnEnable()
    {
        // Tile Palette 열고 CustomEditor와 붙여주기
        EditorApplication.ExecuteMenuItem("Window/2D/Tile Palette");

        rootParent = GameObject.FindGameObjectWithTag("TileRoot")?.transform;
        //Tilemap 찾기
        floorTile = GameObject.FindGameObjectWithTag("Floor")?.GetComponent<Tilemap>();
        if (floorTile != null)
            floorParent = floorTile.transform.parent;
        backGroundTile = GameObject.FindGameObjectWithTag("BackGround")?.GetComponent<Tilemap>();
        if (backGroundTile != null)
            backGroundParent = backGroundTile.transform.parent;
        interactTile = GameObject.FindGameObjectWithTag("Interact")?.GetComponent<Tilemap>();
        if (interactTile != null)
            interactParent = interactTile.transform.parent;
    }
    private void OnGUI()
    {
        GUILayout.Label("Tilemap Settings", EditorStyles.boldLabel);
        // 레벨 선택 -- 레벨별 파일 로드 + 룰타일 로드
        mapLevel = EditorGUILayout.Popup("Map Level", mapLevel, mapFolder);
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Reset Scene", GUILayout.Width(100f), GUILayout.Height(20f)))
        {
            if (EditorUtility.DisplayDialog("Reset Scene", "Are you sure you want to reset the scene?", "Yes", "No"))
            {
                if(rootParent != null)
                {
                    DestroyImmediate(rootParent.gameObject);
                    return;
                }
                if (floorParent != null)
                    DestroyImmediate(floorParent.gameObject);
                if (backGroundParent != null)
                    DestroyImmediate(backGroundParent.gameObject);
                List<GameObject> tilemaps = new List<GameObject>();
                tilemaps.AddRange(GameObject.FindGameObjectsWithTag("Interact"));
                foreach (GameObject tilemap in tilemaps)
                {
                    DestroyImmediate(tilemap.transform.parent.gameObject);
                }
            }
        }
        if (GUILayout.Button("Save Map Prefab", GUILayout.Width(120f), GUILayout.Height(20f)))
        {
            SaveMapPrefab();
        }
        if (GUILayout.Button("Load Map Prefab", GUILayout.Width(120f), GUILayout.Height(20f)))
        {
            LoadMapPrefab();
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.Space(guiSpace);

        guiSpace = EditorGUILayout.IntSlider("Space", guiSpace, 0, 30);

        #region Floor Options
        DrawTilemapOptions(LayerType.Floor, ref floorTile, ref newFloorTileName, ref floorParent, ref floorIsTrigger, "New Floor Name", "Basemap", true, true);
        #endregion
        EditorGUILayout.Space(guiSpace);
        
        #region Interact Options
        DrawTilemapOptions(LayerType.Interact, ref interactTile, ref newInteractTileName, ref interactParent, ref interactIsTrigger, "New Platform Name", "Interact Tilemap", true, false);
        #endregion
        EditorGUILayout.Space(guiSpace);

        #region BackGround Options
        DrawTilemapOptions(LayerType.BackGround, ref backGroundTile, ref newBGTileName, ref backGroundParent, ref dummyTrigger, "New BackGround Name", "BackGround Tilemap", false, true);
        #endregion

    }

    private void DrawTilemapOptions(LayerType layerType, ref Tilemap tilemap, ref string newTileName, ref Transform parentTransform, ref bool isTrigger, string newNameLabel, string objectFieldLabel, bool hasTrigger = false, bool hasSortingOrder = false)
    {
        toggleSwitch[(int)layerType] = EditorGUILayout.ToggleLeft(layerType.ToString(), toggleSwitch[(int)layerType] == 1) ? 1 : 0;

        if (EditorGUILayout.BeginFadeGroup(toggleSwitch[(int)layerType]))
        {
            if (hasTrigger)
            {
                isTrigger = EditorGUILayout.Toggle("Is Trigger", isTrigger);
            }

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Create", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                if (parentTransform == null)
                    tilemap = CreateArea(layerType, null, newTileName).GetComponent<Tilemap>();
                else
                    tilemap = CreateTilemap(layerType, null, newTileName, parentTransform).GetComponent<Tilemap>();
                newTileName = string.Empty;
            }
            newTileName = EditorGUILayout.TextField(newNameLabel, newTileName);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Load", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                Tilemap loadTile = LoadPrefab(layerType, parentTransform == null);
                if (loadTile != null)
                {
                    tilemap = loadTile;
                    if (parentTransform != null)
                        tilemap.transform.parent = parentTransform;
                }
            }

            if (tilemap != null)
            {
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(tilemap);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(tilemap);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) tilemap.ClearAllTiles();
                if (GUILayout.Button("Destroy", GUILayout.Width(60f), GUILayout.Height(15f)))
                {
                    if (layerType == LayerType.Interact)
                        DestroyImmediate(tilemap.transform.parent.gameObject);
                    else
                        DestroyImmediate(tilemap.gameObject);

                    tilemap = GameObject.FindGameObjectWithTag(layerType.ToString())?.GetComponent<Tilemap>();
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            tilemap = (Tilemap)EditorGUILayout.ObjectField(objectFieldLabel, tilemap, typeof(Tilemap), true);
            if (hasSortingOrder && tilemap != null)
                tilemap.GetComponent<TilemapRenderer>().sortingOrder = EditorGUILayout.IntField("Sorting Order", tilemap.GetComponent<TilemapRenderer>().sortingOrder);
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndFadeGroup();
    }

    private GameObject CreateArea(LayerType layerType = LayerType.None, GameObject tilemapPrefab = null, string tileMapName = null)
    {
        // 부모 오브젝트 생성
        GameObject parent = new GameObject("Tilemap");
        parent.transform.position = Vector3.zero;
        parent.transform.rotation = Quaternion.identity;
        parent.transform.localScale = Vector3.one;
        //parent.tag = layerType.ToString();
        parent.layer = LayerMask.NameToLayer(layerType.ToString());
        parent.AddComponent<Grid>();
        if (layerType != LayerType.BackGround)
        {
            parent.AddComponent<Rigidbody2D>();
            parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            parent.AddComponent<CompositeCollider2D>();
            parent.GetComponent<CompositeCollider2D>().geometryType = CompositeCollider2D.GeometryType.Polygons;
            bool isTriggerOn = false;
            if (layerType == LayerType.Floor)
            {
                isTriggerOn = floorIsTrigger;
            }
            else if (layerType == LayerType.Interact)
            {
                isTriggerOn = interactIsTrigger;
            }
            parent.GetComponent<CompositeCollider2D>().isTrigger = isTriggerOn;
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
                    bool isTriggerOn = false;
                    if (layerType == LayerType.Floor)
                    {
                        isTriggerOn = floorIsTrigger;
                    }
                    else if (layerType == LayerType.Interact)
                    {
                        isTriggerOn = interactIsTrigger;
                    }
                    tilemapObject.GetComponent<TilemapCollider2D>().isTrigger = isTriggerOn;
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
    private void SaveMapPrefab()
    {
        if (floorParent == null) return;
        string prefabPath = EditorUtility.SaveFilePanel(
        "Save Map",
        "Assets/CustomTileMapEditor/MapPrefab",
        "",
        "prefab"
         );
        if (string.IsNullOrEmpty(prefabPath))
        {
            Debug.Log("Save operation cancelled.");
            return;
        }
        GameObject mapPrefab = null;
        if (rootParent == null)
        {
            mapPrefab = new GameObject(mapFolder[mapLevel]);
            mapPrefab.tag = "TileRoot";
            floorParent?.SetParent(mapPrefab.transform);
            backGroundParent?.SetParent(mapPrefab.transform);
            List<GameObject> tilemaps = new List<GameObject>();
            tilemaps.AddRange(GameObject.FindGameObjectsWithTag("Interact"));
            foreach (GameObject tilemap in tilemaps)
            {
                tilemap.transform.parent.SetParent(mapPrefab.transform);
            }
        }
        else
        {
            mapPrefab = rootParent.gameObject;
            mapPrefab.name = mapFolder[mapLevel];
        }
        PrefabUtility.SaveAsPrefabAsset(mapPrefab, prefabPath);
    }
    private void LoadMapPrefab()
    {
        string prefabPath = EditorUtility.OpenFilePanel(
        "Load Map",
        "Assets/CustomTileMapEditor/MapPrefab",
        "prefab"
        );
        if (!string.IsNullOrEmpty(prefabPath))
        {
            GameObject prefab = PrefabUtility.LoadPrefabContents(prefabPath);
            if (prefab != null)
            {
                GameObject newMap = Instantiate(prefab);
                rootParent = newMap.transform;
                Transform[] children = newMap.GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if (child.CompareTag("Floor"))
                    {
                        floorTile = child.GetComponent<Tilemap>();
                        floorParent = child.parent;
                    }
                    else if (child.CompareTag("BackGround"))
                    {
                        backGroundTile = child.GetComponent<Tilemap>();
                        backGroundParent = child.parent;
                    }
                    else if (child.CompareTag("Interact"))
                    {
                        interactTile = child.GetComponent<Tilemap>();
                        interactParent = child.parent;
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to load prefab from path: " + prefabPath);
            }
        }
        else
        {
            Debug.Log("Load operation cancelled.");
        }
    }
    private void SavePrefab(Tilemap saveTile)
    {
        if (saveTile == null)
        {
            Debug.LogError("Tilemap is not assigned.");
            return;
        }
        string prefabPath = EditorUtility.SaveFilePanel(
        "Save Tilemap Data",
        "Assets/CustomTileMapEditor/MapData/" + mapFolder[mapLevel],
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
        "Assets/CustomTileMapEditor/MapData/" + mapFolder[mapLevel],
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
        "Assets/CustomTileMapEditor/MapData/" + mapFolder[mapLevel],
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

