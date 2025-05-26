using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolManager : SingleTon<ObjectPoolManager>
{
    private Dictionary<int, ObjectPool> dictPool = new Dictionary<int, ObjectPool>();
    
    public T GetObject<T>()
    {
        GameObject obj = dictPool[0].GetObject();
        return obj.GetComponent<T>();
    }

    public void SetPool(int key, GameObject prefab)
    {
        ObjectPool objectPool = new ObjectPool(CreateObject, prefab);
        dictPool.Add(key, objectPool);
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
