using Orpul1;
using Orpul1.Camera;
using Orpul1.Camera.State;
using Orpul1.State;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour,ISetDataAble<ECameraEffect>
{
    [SerializeField] StateController stateController;
    [SerializeField] CameraContext context;

    private void Awake()
    {
        CameraManager.Instance.SetCamera((int)ECameraType.MainCamera, this.GetComponent<Camera>());
    }
    private void Start()
    {
        stateController.TransitionState((int)ECameraState.Follow);
    }
    private void LateUpdate()
    {
        stateController.ProcessState();
    }

    public void SetValue(ECameraEffect effectType)
    {
        context.cameraEffect = effectType;
    }
}
