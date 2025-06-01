using Orpul1.Camera;
using Orpul1.Camera.State;
using Orpul1.State;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] StateController stateController;
    [SerializeField] CameraContext context;
    [SerializeField] GameObject viewBoundary;
     
    private void Awake()
    {
        CameraManager.Instance.SetCamera((int)ECameraType.MainCamera, this.GetComponent<Camera>());

        context.cameraView.y = context.mainCam.orthographicSize;
        context.cameraView.x = context.mainCam.orthographicSize * ((float)Screen.width / Screen.height);

        IGetValue<List<Transform>> getvalue = viewBoundary.GetComponent<IGetValue<List<Transform>>>();
        if (getvalue == null) return;
        context.viewBoundary = getvalue.GetValue();
    }
    private void Start()
    {
        stateController.TransitionState((int)ECameraState.Follow);
        context.target = PlayerManager.Instance.GetPlayer().transform;
    }
    private void LateUpdate()
    {
        stateController.ProcessState();
    }
}
