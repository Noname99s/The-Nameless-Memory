using UnityEngine;
using System;
namespace Orpul1.Player.State
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) {}

        public override void OnStart()
        {
        }
        public override void OnUpdate()
        {
            OnAir();
        }
        public override void OnExit()
        {
        }
        private void OnAir()
        {
            if (!data.isFalling) return;
            transitionTo((int)EPlayerState.Fall);
        }
    }
}
