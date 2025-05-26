using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }
    int _order = 0;
    [SerializeField] GameObject[] uiPrefabs;
    Dictionary<Type , UIPopup> _popupDictionary = new Dictionary<Type, UIPopup>();

    //public void Init()
    //{
    //    _order = 0;
    //}
    #region юс╫ц
    private void Start()
    {
        _order = 0;
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Show Main"))
        {
            ShowPopupUI<MainMenuView>();
        }
        if (GUILayout.Button("Show Loading"))
        {
            ShowPopupUI<LoadingView>();
        }
        if (GUILayout.Button("Close Main"))
        {
            ClosePopupUI<MainMenuView>();
        }
        if (GUILayout.Button("Close Loading"))
        {
            ClosePopupUI<LoadingView>();
        }
        if (GUILayout.Button("Close All"))
        {
            CloseAllPopupUI();
        }
    }
    #endregion

    public T GetPopupUI<T>() where T : UIPopup
    {
        if (_popupDictionary.TryGetValue(typeof(T), out UIPopup popup))
        {
            return popup as T;
        }
        return ShowPopupUI<T>();
    }

    public T ShowPopupUI<T>() where T : UIPopup
    {
        GameObject go = _popupDictionary.TryGetValue(typeof(T), out UIPopup popup) ? popup.gameObject : null;

        if (go == null)
        {
            for(int i = 0; i < uiPrefabs.Length; i++)
            {
                if (uiPrefabs[i].GetComponent<T>() != null)
                {
                    go = Instantiate(uiPrefabs[i], Root.transform);
                    break;
                }
            }
            T sceneUI = Util.GetOrAddComponent<T>(go);
            sceneUI.Init();
            _popupDictionary[typeof(T)] = sceneUI;
        }
        go.SetActive(true);
        
        return _popupDictionary[typeof(T)] as T;
    }
    public void ClosePopupUI<T>() where T : UIPopup
    {
        GameObject go = _popupDictionary.TryGetValue(typeof(T), out UIPopup popup) ? popup.gameObject : null;
        if (go != null)
        {
            _order--;
            go.SetActive(false);
        }
    }
    public void CloseAllPopupUI()
    {
        for(int i = 0; i < _popupDictionary.Count; i++)
        {
            _popupDictionary.ElementAt(i).Value.gameObject.SetActive(false);
        }
        _order = 0;
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            _order++;
            canvas.sortingOrder = _order;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
}
