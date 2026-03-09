using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UIManager
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
    UIData _uiData = new UIData();
    int _order = 0;
    Dictionary<Type , UIPopup> _popupDictionary = new Dictionary<Type, UIPopup>();

    public void Init()
    {
        _order = 0;
        _uiData.Init();
    }

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
            //string prefabName = typeof(T).Name;
            //int index = prefabName.IndexOf("Composition");
            //if (index > 0)
            //{
            //    prefabName = prefabName.Substring(0, index);
            //}
            go = Managers.Resource.Instantiate(_uiData.prefabList[typeof(T)], Root.transform);
            T sceneUI = Util.GetOrAddComponent<T>(go);
            sceneUI.Init();
            _popupDictionary[typeof(T)] = sceneUI;
            if (go == null)
            {
                Debug.LogError($"Failed to load prefab for {typeof(T).Name}");
                return null;
            }
        }
        go.SetActive(true);
        SetCanvas(go, true);

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
