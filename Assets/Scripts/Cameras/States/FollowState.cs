using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Orpul1.Camera
{
    public class FollowState : CameraState
    {
        public FollowState(CameraContext data, Action<int> transitionTo) : base(data, transitionTo) {}
        
        public override void OnStart()
        {
            data.cinemachineCamera.Target = data.cameraTarget;
        }

        public override void OnUpdate()
        {
         /*   Vector2 dir = ((Vector2)data.target.position - data.rigid.position);
            data.rigid.MovePosition(data.rigid.position + dir * data.moveSpeed*Time.deltaTime);
            */
        }

        public override void OnExit()
        {
        }
    }
}