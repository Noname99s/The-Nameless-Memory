using UnityEngine;
using System.Collections.Generic;
using System;

public class UIData
{
    public Dictionary<Type, GameObject> prefabList = new Dictionary<Type, GameObject>();

    public void Init()
    {
        int listCount = BG_DB_UIData.CountEntities;
        for (int i = 0; i < listCount; i++)
        {
            GameObject entity = BG_DB_UIData.GetEntity(i)._Prefab;
            if (entity != null)
            {
                UIPopup popupComponent = entity.GetComponent<UIPopup>();
                if (popupComponent != null)
                {
                    Type type = popupComponent.GetType();
                    prefabList[type] = entity;
                }
            }
        }
    }
}
