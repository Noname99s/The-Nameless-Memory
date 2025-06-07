using Orpul1;
using Orpul1.Camera;
using System.Collections;
using UnityEngine;

public abstract class TrackingCameraBase : MonoBehaviour
{
    IProcessAble _playerSetter;
    ISetDataAble<Transform> _cameraSetter;
    public void Init()
    {
        _playerSetter = Managers.Player.GetPlayer().GetComponent<IProcessAble>();
        _cameraSetter = Managers.Camera.GetCamera<ISetDataAble<Transform>>((int)ECameraType.MainCamera);
    }
    public void TrackingStart()
    {
        if (_playerSetter == null || _cameraSetter == null)
        {
            Debug.LogError("Player or Camera Setter is not initialized.");
            return;
        }
        // ∏ÿ√„
        _playerSetter.Process();
        _cameraSetter.SetValue(this.transform);
    }
    public void TrackingEnd()
    {
        if (_playerSetter == null || _cameraSetter == null)
        {
            Debug.LogError("Player or Camera Setter is not initialized.");
            return;
        }
        // ∏ÿ√„ «ÿ¡¶
        _playerSetter.Abort();
        _cameraSetter.SetValue(Managers.Player.GetPlayer().transform);
    }
    public abstract IEnumerator TrackingScene();
}
