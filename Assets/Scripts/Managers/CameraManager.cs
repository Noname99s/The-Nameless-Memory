using System;
using System.Collections.Generic;
using Orpul1.Camera;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : SingleTon<CameraManager>
{
    Dictionary<int, GameObject> dictCamera = new Dictionary<int, GameObject>();
    UnityEvent setCameraEvent = new UnityEvent();
    public void SetCamera(int key, GameObject camera)
    {
        if (dictCamera.ContainsKey(key))
            return;
        dictCamera.Add(key, camera);
        if (key == (int)ECameraType.MainCamera)
            ExecuteEvent();
    }
    public void SetCameraEvent(UnityAction action)
    {
        setCameraEvent.AddListener(action);
    }
    public T GetCamera<T>(int key)
    {
        if (!dictCamera.ContainsKey(key))
            return default(T);
        
        return dictCamera[key].GetComponent<T>();
    }
    public void RemoveCamera(int key)
    {
        dictCamera.Remove(key);
        setCameraEvent.RemoveAllListeners();
    }

    private void ExecuteEvent()
    {
        setCameraEvent?.Invoke();
    }

}
