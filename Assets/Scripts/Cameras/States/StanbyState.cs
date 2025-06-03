using System;
using Orpul1.Camera;
using UnityEngine;

public class StanbyState : CameraState
{
    public StanbyState(CameraContext data, Action<int> transitionTo) : base(data, transitionTo)
    {
    }

    public override void OnStart()
    {
        data.cinemachineCamera.Lens.OrthographicSize = 8;
    }

    public override void OnUpdate()
    {
    }
    public override void OnExit()
    {
        data.cinemachineCamera.Lens.OrthographicSize = 5;
    }

   
}
