using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> pools = new Queue<GameObject>();
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject parent;
    private void Start()
    {
        Initialize();
    }
    protected void Initialize()
    {
        if (prefab == null) return;
        for (int i = 0; i < 5; i++)
        {
            CreateObject();
        }
    }
    protected void EnqueueObject(GameObject obj)
    {
        obj.SetActive(false);
        pools.Enqueue(obj);
    }
    protected T GetObject<T>()
    {
        if (pools.Count <= 0)
            CreateObject();

        GameObject pool = pools.Dequeue();
        pool.SetActive(true);
        return pool.GetComponent<T>();
    }
    private void CreateObject()
    {
        GameObject pool = Instantiate(prefab);
        pool.transform.SetParent(parent.transform);
        pool.SetActive(false);
        pools.Enqueue(pool);
    }
    protected void Release()
    {
        int size = pools.Count;
        for (int i = 0; i < size; i++)
        {
            Destroy(pools.Dequeue());
        }
    }
}
