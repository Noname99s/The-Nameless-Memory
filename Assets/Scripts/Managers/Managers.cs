using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
public class Managers : MonoBehaviour
{
    static Managers s_instance;
    GameManager _game = new GameManager();
    GameDataManager _gameData = new GameDataManager();
    SceneLoadManager _sceneLoad = new SceneLoadManager();
    CameraManager _camera = new CameraManager();
    //SoundManager _sound;

    UIManager _ui = new UIManager();
    [SerializeField] ResourceManager _resource;
    [SerializeField] MapManager _mapManager;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] SoundManager _soundManager;

    public static GameManager Game { get { return Instance._game; } }
    public static GameDataManager GameData { get { return Instance._gameData; } }
    public static SceneLoadManager SceneLoad { get { return Instance._sceneLoad; } }
    public static CameraManager Camera { get { return Instance._camera; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static MapManager Map { get { return Instance._mapManager; } }
    public static PlayerManager Player { get { return Instance._playerManager; } }
    public static SoundManager Sound { get { return Instance._soundManager; } }

    static Managers Instance 
    { 
        get 
        {
            if (s_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    // Hierarchi 코드로 오브젝트 만들고 스크립트 넣음
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                s_instance = go.GetComponent<Managers>();
            }
            return s_instance;
        } 
    }
    public void Init()
    {
        s_instance = GetComponent<Managers>();
        _game.Init();
        _mapManager.Init();
        _sceneLoad.Init();
        _ui.Init();
        _soundManager.Init();
    }
    private void Start()
    {
        Init();
    }
    void Update()
    {
        _game.UpdatePlayTime(); 
    }
    void OnDestroy()
    {
        if (s_instance == this)
        {
            s_instance = null;
        }
    }
}
