using System;
using System.Collections.Generic;
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

    Dictionary<Type , UIPopup> _popupDictionary = new Dictionary<Type, UIPopup>();

    public void Init()
    {
        _order = 0;
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
        GameObject go = _popupDictionary[typeof(T)]?.gameObject;
        if (go == null)
        {
            //go = Managers.Resource.Instantiate(FactoryType.UI, (int)name, Root.transform);
            T sceneUI = Util.GetOrAddComponent<T>(go);
            sceneUI.Init();
            _popupDictionary[typeof(T)] = sceneUI;
        }
        go.SetActive(true);
        
        return _popupDictionary[typeof(T)] as T;
    }
    public void ClosePopupUI<T>() where T : UIPopup
    {
        GameObject go = _popupDictionary[typeof(T)]?.gameObject;
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
            ClosePopupUI<UIPopup>();
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
