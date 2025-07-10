using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private Queue<GameObject> pools = new Queue<GameObject>();
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected GameObject parent;
    public Queue<GameObject> Pools {  get { return pools; } }
    protected virtual void Start()
    {
        Initialize();
    }
    protected virtual void Initialize()
    {
        if (prefab == null) return;
        for (int i = 0; i < 5; i++)
        {
            CreateObject();
        }
    }
    protected virtual void EnqueueObject(GameObject obj)
    {
        Debug.Log("EnqueueAction!");
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
