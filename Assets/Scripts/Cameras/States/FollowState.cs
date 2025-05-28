using Orpul1.Camera.State;
using Orpul1.State;
using System;
using UnityEngine;
namespace Orpul1.Camera
{
    public class FollowState : CameraState
    {
        public FollowState(CameraContext data, Action<int> transitionTo) : base(data, transitionTo) {}

        public override void OnStart()
        {
        }

        public override void OnUpdate()
        {
            Vector2 dir = ((Vector2)data.target.position - data.rigid.position);
            data.rigid.MovePosition(data.rigid.position + dir * data.moveSpeed*Time.deltaTime);
            TransitionState();
        }

        public override void OnExit()
        {
        }
        private void TransitionState()
        {
            if (IsVignetteState()) return;
        }
        private bool IsVignetteState()
        {
            if (!data.IsVignetteState)
                return false;
            transitionTo((int)ECameraState.VignetteIn);

            return true;
        }
    }
}