using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> pools = new Queue<GameObject>();
    private GameObject prefab;
    private GameObject parent;
    private Func<GameObject, GameObject> createObject;
    public ObjectPool(Func<GameObject,GameObject> createObject, GameObject prefab)
    {
        this.createObject = createObject;
        Initialize();
    }
    private void Initialize()
    {
        parent = new GameObject(prefab.name);
        for (int i = 0; i < 5; i++)
        {
            CreateObject();
        }
    }
    public GameObject GetObject()
    {
        if (pools.Count <= 0)
            CreateObject();
        return pools.Dequeue();
    }
    private void CreateObject()
    {
        GameObject pool = createObject?.Invoke(prefab);
        pool.transform.SetParent(parent.transform);
            
        pools.Enqueue(pool);
    }
    public void Release()
    {
        int size = pools.Count;
        for (int i = 0; i < size; i++)
        {
            //TODO IDestroyAble 인터페이스를 통해 Release 해주기
            Destroy(pools.Dequeue());
        }
    }
}
