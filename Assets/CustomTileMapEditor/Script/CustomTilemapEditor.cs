using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CustomTilemapEditor : EditorWindow
{
    enum LayerType
    {
        None,
        BackGround1,
        BackGround2,
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
    Tilemap backGroundTile1;
    Tilemap backGroundTile2;

    int guiSpace = 20;
    int mapLevel = 0;
    int[] toggleSwitch = new int[(int)LayerType.Max];
    string newInteractTileName = string.Empty;

    private void OnEnable()
    {
        // Tile Palette 열고 CustomEditor와 붙여주기
        EditorApplication.ExecuteMenuItem("Window/2D/Tile Palette");

        //Tilemap 찾기
        floorTile = GameObject.FindGameObjectWithTag("Floor")?.GetComponent<Tilemap>();
        if (floorTile == null)
            floorTile = CreateArea(LayerType.Floor).GetComponent<Tilemap>();
        backGroundTile1 = GameObject.FindGameObjectWithTag("BackGround1")?.GetComponent<Tilemap>();
        backGroundTile2 = GameObject.FindGameObjectWithTag("BackGround2")?.GetComponent<Tilemap>();
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
                List<GameObject> tilemaps = new List<GameObject>();
                tilemaps.AddRange(GameObject.FindGameObjectsWithTag("Floor"));
                tilemaps.AddRange(GameObject.FindGameObjectsWithTag("Interact"));
                tilemaps.AddRange(GameObject.FindGameObjectsWithTag("BackGround1"));
                tilemaps.AddRange(GameObject.FindGameObjectsWithTag("BackGround2"));
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
            if (floorTile == null)
            {
                floorTile = GameObject.FindGameObjectWithTag("Floor")?.GetComponent<Tilemap>();
                if (floorTile == null)
                {
                    GUILayout.Label("Floor : Empty", EditorStyles.boldLabel);
                    GUILayout.BeginHorizontal("box");
                    if (GUILayout.Button("Create", GUILayout.Width(50f), GUILayout.Height(15f)))
                        floorTile = CreateArea(LayerType.Floor).GetComponent<Tilemap>();
                    if (GUILayout.Button("Load", GUILayout.Width(50f), GUILayout.Height(15f)))
                        floorTile = LoadPrefab(LayerType.Floor);
                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(floorTile);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(floorTile);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) floorTile.ClearAllTiles();
                GUILayout.EndHorizontal();
                floorTile = (Tilemap)EditorGUILayout.ObjectField("Basemap", floorTile, typeof(Tilemap), true);
            }
        }
        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.Space(guiSpace);
        #endregion

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
                interactTile = LoadPrefab(LayerType.Interact);
            }
            if (interactTile != null)
            {
                if (GUILayout.Button("Destroy", GUILayout.Width(60f), GUILayout.Height(15f)))
                {
                    DestroyImmediate(interactTile.transform.parent.gameObject);
                    interactTile = GameObject.FindGameObjectWithTag("Interact")?.GetComponent<Tilemap>();
                }
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(interactTile);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(interactTile);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) interactTile.ClearAllTiles();
            }
            GUILayout.EndHorizontal();
            interactTile = (Tilemap)EditorGUILayout.ObjectField("Interact Tilemap", interactTile, typeof(Tilemap), true);
        }
        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.Space(guiSpace);
        #endregion

        #region BackGround1 Options
        toggleSwitch[(int)LayerType.BackGround1] = EditorGUILayout.ToggleLeft("BackGround1", toggleSwitch[(int)LayerType.BackGround1] == 1) ? 1 : 0;
        if (EditorGUILayout.BeginFadeGroup(toggleSwitch[(int)LayerType.BackGround1]))
        {
            if (backGroundTile1 == null)
                backGroundTile1 = GameObject.FindGameObjectWithTag("BackGround1")?.GetComponent<Tilemap>();
            if (backGroundTile1 == null)
            {
                GUILayout.Label("BackGround1 : Empty", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Create", GUILayout.Width(50f), GUILayout.Height(15f)))
                    backGroundTile1 = CreateArea(LayerType.BackGround1).GetComponent<Tilemap>();
                if (GUILayout.Button("Load", GUILayout.Width(50f), GUILayout.Height(15f)))
                    backGroundTile1 = LoadPrefab(LayerType.BackGround1);
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(backGroundTile1);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(backGroundTile1);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) backGroundTile1.ClearAllTiles();
                GUILayout.EndHorizontal();
            }
            backGroundTile1 = (Tilemap)EditorGUILayout.ObjectField("BackGround1 Tilemap", backGroundTile1, typeof(Tilemap), true);
        }
        EditorGUILayout.EndFadeGroup();
        #endregion

        #region BackGround2 Options
        toggleSwitch[(int)LayerType.BackGround2] = EditorGUILayout.ToggleLeft("BackGround2", toggleSwitch[(int)LayerType.BackGround2] == 1) ? 1 : 0;
        if (EditorGUILayout.BeginFadeGroup(toggleSwitch[(int)LayerType.BackGround2]))
        {
            if (backGroundTile2 == null)
                backGroundTile2 = GameObject.FindGameObjectWithTag("BackGround2")?.GetComponent<Tilemap>();
            if (backGroundTile2 == null)
            {
                GUILayout.Label("BackGround2 : Empty", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Create", GUILayout.Width(50f), GUILayout.Height(15f)))
                    backGroundTile2 = CreateArea(LayerType.BackGround2).GetComponent<Tilemap>();
                if (GUILayout.Button("Load", GUILayout.Width(50f), GUILayout.Height(15f)))
                    backGroundTile2 = LoadPrefab(LayerType.BackGround2);
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Save", GUILayout.Width(50f), GUILayout.Height(15f))) SavePrefab(backGroundTile2);
                if (GUILayout.Button("Paint", GUILayout.Width(50f), GUILayout.Height(15f))) Paint(backGroundTile2);
                if (GUILayout.Button("Clear", GUILayout.Width(50f), GUILayout.Height(15f))) backGroundTile2.ClearAllTiles();
                GUILayout.EndHorizontal();
            }
            backGroundTile2 = (Tilemap)EditorGUILayout.ObjectField("BackGround2 Tilemap", backGroundTile2, typeof(Tilemap), true);
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
        if (layerType != LayerType.BackGround1)
        {
            parent.AddComponent<Rigidbody2D>();
            parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            parent.AddComponent<CompositeCollider2D>();
        }

        // 자식 오브젝트 생성
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
                if (layerType != LayerType.BackGround1)
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
        PrefabUtility.SaveAsPrefabAsset(saveTile.gameObject, prefabPath);
        Debug.Log("Prefab saved at: " + prefabPath);
    }
    private Tilemap LoadPrefab(LayerType layerType)
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
            Tilemap loadTile = CreateArea(layerType, prefab, prefabName).GetComponent<Tilemap>();
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
        paintMap.ClearAllTiles();
        if (!string.IsNullOrEmpty(prefabPath))
        {
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
    }
    #endregion
}

