using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
public class Managers : MonoBehaviour
{
    static Managers s_instance;
    //SceneLoadManager _sceneLoad = new SceneLoadManager();
    //SoundManager _sound;

    UIManager _ui = new UIManager();
    [SerializeField] ResourceManager _resource;
    
    //public static SceneLoadManager SceneLoad { get { return Instance._sceneLoad; } }

    public static UIManager UI { get { return Instance._ui; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
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
        //_sceneLoad.Init();
        s_instance = GetComponent<Managers>();
        _ui.Init();
        _ui.ShowPopupUI<MainMenuView>();
    }
    private void Start()
    {
        Init();
    }
    void OnDestroy()
    {
        if (s_instance == this)
        {
            s_instance = null;
        }
    }
}
