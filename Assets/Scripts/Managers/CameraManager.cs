using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingleTon<CameraManager>
{
    Dictionary<int, Camera> dictCamera = new Dictionary<int, Camera>();
    public void SetCamera(int key, Camera camera)
    {
        if (dictCamera.ContainsKey(key))
            return;
        dictCamera.Add(key, camera);
    }
    public Camera GetCamera(int key)
    {
        if (!dictCamera.ContainsKey(key))
            return null;
        return dictCamera[key];
    }
}
