using System;
using Orpul1.Player;
using Orpul1.State;
using UnityEngine;

namespace Orpul1.Player.State
{
    public abstract class PlayerBaseState : IState
    {
        protected PlayerContext data;
        protected Action<int> transitionTo;
        public PlayerBaseState(PlayerContext playerContext, Action<int> transitionTo)
        {
            this.data = playerContext;
            this.transitionTo = transitionTo;
        }

        public abstract void OnExit();

        public abstract void OnStart();

        public abstract void OnUpdate();
        protected abstract void TransitionState();
    }

}
