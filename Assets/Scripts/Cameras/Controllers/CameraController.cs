using Orpul1;
using Orpul1.Camera;
using Orpul1.Camera.State;
using Orpul1.State;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour,ISetDataAble<Collider2D>
{
    [SerializeField] StateController stateController;
    [SerializeField] CameraContext context;
     
    private void Awake()
    {
        CameraManager.Instance.SetCamera((int)ECameraType.MainCamera, this.GetComponent<Camera>());

        context.cameraView.y = context.mainCam.orthographicSize;
        context.cameraView.x = context.mainCam.orthographicSize * ((float)Screen.width / Screen.height);

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

    public void SetValue(Collider2D confinder)
    {
        context.confiner.BoundingShape2D = confinder;
    }
}
