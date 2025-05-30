using UnityEngine;
using Orpul1.State;
using System;
namespace Orpul1.Camera
{
    public abstract class CameraState : IState
    {
        protected CameraContext data;
        protected Action<int> transitionTo;
        public CameraState(CameraContext data, Action<int> transitionTo) 
        {
            this.data = data;
            this.transitionTo = transitionTo;
        }
        public abstract void OnStart();
        public abstract void OnUpdate();

        public abstract void OnExit();
    }
}
