using System;
using System.Data.Common;
using Orpul1.State;
using UnityEditor.Build;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class MoveState : PlayerBaseState
    {
        public MoveState(PlayerContext playerdata, Action<int> transitionTo) : base(playerdata, transitionTo) {}
        public override void OnStart()
        {
        }
        public override void OnUpdate()
        {
            float dirX = data.moveDir.x * data.moveSpeed * Time.deltaTime;

            DoSlideMove(dirX);
            DoMove(dirX);
            TransitionState();
        }
        public override void OnExit()
        {
            data.rigid.linearVelocity = new Vector2(0, data.rigid.linearVelocityY) ;
        }

        private void DoSlideMove(float dirX)
        {
            if (data.isPressJump && data.jumpState == EJumpState.JumpReady) return;
            data.slideResult = data.rigid.Slide(Vector2.right * dirX, Time.deltaTime, data.slideMovement);
        }
        private void DoMove(float dirX)
        {
            if (!data.isPressJump || data.jumpState != EJumpState.JumpReady) return;
            data.rigid.linearVelocityX = dirX;
        }
        
        private void TransitionState()
        {
            if (IsIdleState()) return;
            if (IsJumpState()) return;
            if (IsFallState()) return;
        }
        private bool IsJumpState()
        {
            if (!data.IsJumpState) return false;
            transitionTo((int)EPlayerState.Jump);

            return true;
        }
        private bool IsFallState()
        {
            if (!data.isFalling) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }
        private bool IsIdleState()
        {
            if (!data.IsIdleState) return false;
            transitionTo((int)EPlayerState.Idle);

            return true;
        }
    }

}
