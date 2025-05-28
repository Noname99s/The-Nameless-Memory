using Orpul1.State;
using System;
using UnityEngine;
namespace Orpul1.Camera
{
    public class Follow : CameraState
    {
        public Follow(CameraContext data, Action<int> transitionTo) : base(data, transitionTo) {}

        public override void OnStart()
        {
        }

        public override void OnUpdate()
        {
            Vector2 dir = ((Vector2)data.target.position - data.rigid.position);
            data.rigid.MovePosition(data.rigid.position + dir * data.moveSpeed*Time.deltaTime);
        }

        public override void OnExit()
        {
        }
    }
}