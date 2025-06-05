using Orpul1;
using Orpul1.Camera;
using Orpul1.Camera.State;
using Orpul1.State;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour,ISetDataAble<Collider2D>, ISetDataAble<Transform>, ISetCamerGroupAble
{
    [SerializeField] StateController stateController;
    [SerializeField] CameraContext context;
     
    private void Awake()
    {
        context.cameraView.y = context.mainCam.orthographicSize;
        context.cameraView.x = context.mainCam.orthographicSize * ((float)Screen.width / Screen.height);
    }
    private void Start()
    {
        TrackingTarget();
    }
    private void OnEnable()
    {
        Managers.Camera.SetCamera((int)ECameraType.MainCamera, gameObject);
    }
    private void OnDisable()
    {
        //RemoveCamera();
    }
    public void TrackingTarget()
    {
        context.cameraTarget.TrackingTarget = Managers.Player.GetPlayer().transform;
        stateController.TransitionState((int)ECameraState.Follow);
    }
    private void RemoveCamera()
    {
        Managers.Camera.RemoveCamera((int)ECameraType.MainCamera);
    }
    private void LateUpdate()
    {
        stateController.ProcessState();
    }

    public void SetValue(Collider2D confinder)
    {
        context.confiner.BoundingShape2D = confinder;
    }

    public void SetGroup(Transform target, float weight, float radius)
    {
        context.targetGroup.AddMember(target, weight, radius);
    }

    public void RemoveGroup(Transform target)
    {
        context.targetGroup.RemoveMember(target);
    }

    public void SetValue(Transform target)
    {
        context.cameraTarget.TrackingTarget = target;
    }
}
