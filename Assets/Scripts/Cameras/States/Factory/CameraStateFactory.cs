using Orpul1.Camera;
using Orpul1.Camera.State;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Orpul1.State
{
    public class CameraStateFactory : StateFactory
    {
        [SerializeField] CameraContext context;
        public override List<IState> Create(Action<int> transitionTo)
        {
            return new List<IState>()
            {
                new FollowState(context, transitionTo),
            };
        }
    }
}
