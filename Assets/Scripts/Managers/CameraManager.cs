using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingleTon<CameraManager>
{
    Dictionary<int, GameObject> dictCamera = new Dictionary<int, GameObject>();
    public void SetCamera(int key, GameObject camera)
    {
        if (dictCamera.ContainsKey(key))
            return;
        dictCamera.Add(key, camera);
    }
    public T GetCamera<T>(int key)
    {
        if (!dictCamera.ContainsKey(key))
            return default(T);
        
        return dictCamera[key].GetComponent<T>();
    }
}
