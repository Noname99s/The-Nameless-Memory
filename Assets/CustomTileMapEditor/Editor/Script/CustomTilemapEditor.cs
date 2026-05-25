using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomTilemapEditor : EditorWindow
{
    [System.Serializable]
    public class LayerData
    {
        public Tilemap tilemap;
        public string newTileName = string.Empty;
        public Transform parentTransform;
        public bool isTrigger = false;
        public bool toggleSwitch = false;
    }

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

    LayerData[] layerDatas = new LayerData[(int)LayerType.Max];

    int guiSpace = 20;
    int mapLevel = 0;

    string[] mapFolder = new string[] { "Hub", "Map1", "Map2", "Map3", "Map4", "Map5" };
    Transform rootParent;

    private void OnEnable()
    {
        for (int i = 0; i < (int)LayerType.Max; i++)
        {
            if (layerDatas[i] == null)
                layerDatas[i] = new LayerData();
        }

        // Tile Palette 열고 CustomEditor와 붙여주기
        EditorApplication.ExecuteMenuItem("Window/2D/Tile Palette");

        rootParent = GameObject.FindGameObjectWithTag("TileRoot")?.transform;

        // Tilemap 찾기
        InitializeLayer(LayerType.Floor);
        InitializeLayer(LayerType.BackGround);
        InitializeLayer(LayerType.Interact);
    }

    private void InitializeLayer(LayerType type)
    {
        LayerData data = layerDatas[(int)type];
        data.tilemap = GameObject.FindGameObjectWithTag(type.ToString())?.GetComponent<Tilemap>();
        if (data.tilemap != null)
            data.parentTransform = data.tilemap.transform.parent;
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

                foreach (var data in layerDatas)
                {
                    if (data?.parentTransform != null)
                    {
                        DestroyImmediate(data.parentTransform.gameObject);
                        data.parentTransform = null;
                        data.tilemap = null;
                    }
                }

                List<GameObject> tilemaps = new List<GameObject>();
                tilemaps.AddRange(GameObject.FindGameObjectsWithTag("Interact"));
                foreach (GameObject tilemap in tilemaps)
                {
                    if (tilemap != null && tilemap.transform.parent != null)
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
        DrawTilemapOptions(LayerType.Floor, "New Floor Name", "Basemap", true, true);
        #endregion
        EditorGUILayout.Space(guiSpace);
        
        #region Interact Options
        DrawTilemapOptions(LayerType.Interact, "New Platform Name", "Interact Tilemap", true, false);
        #endregion
        EditorGUILayout.Space(guiSpace);

        #region BackGround Options
        DrawTilemapOptions(LayerType.BackGround, "New BackGround Name", "BackGround Tilemap", false, true);
        #endregion

    }

    private void DrawTilemapOptions(LayerType layerType, string newNameLabel, string objectFieldLabel, bool hasTrigger = false, bool hasSortingOrder = false)
    {
        LayerData data = layerDatas[(int)layerType];
        data.toggleSwitch = EditorGUILayout.ToggleLeft(layerType.ToString(), data.toggleSwitch);

        if (EditorGUILayout.BeginFadeGroup(data.toggleSwitch ? 1 : 0))
        {
            if (hasTrigger)
            {
                data.isTrigger = EditorGUILayout.Toggle("Is Trigger", data.isTrigger);
            }

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Create", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                if (data.parentTransform == null)
                    data.tilemap = CreateArea(layerType, null, data.newTileName).GetComponent<Tilemap>();
                else
                    data.tilemap = CreateTilemap(layerType, null, data.newTileName, data.parentTransform).GetComponent<Tilemap>();
                data.newTileName = string.Empty;
            }
            data.newTileName = EditorGUILayout.TextField(newNameLabel, data.newTileName);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Load", GUILayout.Width(50f), GUILayout.Height(15f)))
            {
                Tilemap loadTile = LoadPrefab(layerType, data.parentTransform == null);
                if (loadTile != null)
                {
                    data.tilemap = loadTile;
                    if (data.parentTransform != null)
                        data.tilemap.transform.parent = data.parentTransform;
                }
            }

            if (data.tilemap != null)
            {
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(data.tilemap);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(data.tilemap);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) data.tilemap.ClearAllTiles();
                if (GUILayout.Button("Destroy", GUILayout.Width(60f), GUILayout.Height(15f)))
                {
                    data.tilemap.GetComponent<TilemapCollider2D>().compositeOperation = Collider2D.CompositeOperation.None;
                    if (layerType == LayerType.Interact)
                        DestroyImmediate(data.tilemap.transform.parent.gameObject);
                    else
                        DestroyImmediate(data.tilemap.gameObject);
                    data.tilemap = GameObject.FindGameObjectWithTag(layerType.ToString())?.GetComponent<Tilemap>();
                    GUIUtility.ExitGUI();
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            data.tilemap = (Tilemap)EditorGUILayout.ObjectField(objectFieldLabel, data.tilemap, typeof(Tilemap), true);
            if (hasSortingOrder && data.tilemap != null)
            {
                TilemapRenderer tr = data.tilemap.GetComponent<TilemapRenderer>();
                if (tr != null) tr.sortingOrder = EditorGUILayout.IntField("Sorting Order", tr.sortingOrder);
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndFadeGroup();
    }

    private GameObject CreateArea(LayerType layerType = LayerType.None, GameObject tilemapPrefab = null, string tileMapName = null)
    {
        LayerData data = layerDatas[(int)layerType];
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
            parent.GetComponent<CompositeCollider2D>().isTrigger = data.isTrigger;
        }

        if (layerType != LayerType.None && layerType != LayerType.Max)
            data.parentTransform = parent.transform;

        // 자식 오브젝트 생성
        GameObject tilemapObject = CreateTilemap(layerType, tilemapPrefab, tileMapName, parent.transform);
        return tilemapObject;
    }
    private GameObject CreateTilemap(LayerType layerType = LayerType.None, GameObject tilemapPrefab = null, string tileMapName = null, Transform parent = null)
    {
        LayerData data = layerDatas[(int)layerType];
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
                    tilemapObject.GetComponent<TilemapCollider2D>().isTrigger = data.isTrigger;
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
        LayerData floorData = layerDatas[(int)LayerType.Floor];
        LayerData bgData = layerDatas[(int)LayerType.BackGround];

        if (floorData.parentTransform == null) return;
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
            floorData.parentTransform?.SetParent(mapPrefab.transform);
            bgData.parentTransform?.SetParent(mapPrefab.transform);
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
                    foreach (LayerType type in System.Enum.GetValues(typeof(LayerType)))
                    {
                        if (type == LayerType.None || type == LayerType.Max) continue;
                        if (child.CompareTag(type.ToString()))
                        {
                            layerDatas[(int)type].tilemap = child.GetComponent<Tilemap>();
                            layerDatas[(int)type].parentTransform = child.parent;
                        }
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

