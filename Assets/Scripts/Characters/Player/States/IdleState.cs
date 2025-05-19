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
            data.rigid.linearVelocityX = 0;
            TransitionState();
        }
        public override void OnExit()
        {
        }

        private void OnJumpState()
        {
            if (!data.isPressJump || data.jumpState != EJumpState.JumpReady) return;
            transitionTo((int)EPlayerState.Jump);
        }
        private void OnMoveState()
        {
            if (data.movePosi == Vector2.zero) return;
            transitionTo((int)EPlayerState.Move);
        }
        private void OnFallState()
        {
            if (!data.isFalling) return;
            transitionTo((int)EPlayerState.Fall);
        }
        private void TransitionState()
        {
            OnJumpState();
            OnMoveState();
            OnFallState();
        }
    }
}
