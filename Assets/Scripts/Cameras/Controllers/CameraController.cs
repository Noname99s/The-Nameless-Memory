using Orpul1.Camera;
using Orpul1.Camera.State;
using Orpul1.State;
using UnityEngine;

public class CameraController : MonoBehaviour
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
}
