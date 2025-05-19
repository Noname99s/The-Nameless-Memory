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
            float dirX = data.movePosi.x * data.moveSpeed * Time.deltaTime;

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
            if(IsJump())
            {
                transitionTo((int)EPlayerState.Jump);
            }
            else if (data.isFalling)
            {
                transitionTo((int)EPlayerState.Fall);
            }
        }
        private bool IsJump()
        {
            return data.jumpState == EJumpState.Jumping || data.isPressJump;
        }
    }

}
