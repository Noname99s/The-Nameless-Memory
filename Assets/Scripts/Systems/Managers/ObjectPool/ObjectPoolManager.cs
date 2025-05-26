using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolManager : SingleTon<ObjectPoolManager>
{
    private Dictionary<int, ObjectPool> dictPool = new Dictionary<int, ObjectPool>();
    
    public T GetObject<T>(int key)
    {
        if (!dictPool.ContainsKey(key))
            return default(T);
        GameObject obj = dictPool[key].GetObject();
        return obj.GetComponent<T>();
    }
    public void SetObject(int key, GameObject pool)
    {
        if (!dictPool.ContainsKey(key))
            return;

        dictPool[key].SetObject(pool);
    }

    public void SetPool(int key, GameObject prefab)
    {
        if (dictPool.ContainsKey((int)key)) return;
        ObjectPool objectPool = new ObjectPool(CreateObject, prefab);
        dictPool.Add(key, objectPool);
    }
    public void ReleasePool(int key)
    {
        dictPool[key].Release();
        dictPool.Remove(key);
    }
    public void ReleaseAll()
    {
        int size = dictPool.Count;
        for (int i = 0; i < size; i++)
        {
            KeyValuePair<int,ObjectPool> poolKeyValue = dictPool.ElementAt(i);
            poolKeyValue.Value.Release();
            dictPool.Remove(poolKeyValue.Key);
        }
    }

    private GameObject CreateObject(GameObject prefab)
    {
        return Instantiate(prefab);
    }
}
