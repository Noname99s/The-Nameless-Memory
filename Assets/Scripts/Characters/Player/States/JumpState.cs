using Orpul1.Player;
using Orpul1.Player.Animation;
using System;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class JumpState : PlayerBaseState
    {
        float jumpAmount = 0;

        public JumpState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) { }
        public override void OnStart()
        {
            data.animator.SetBool(EPlayerAni.IsGround.ToString(), false);
            data.animator.SetTrigger(EPlayerAni.JumpTrigger.ToString());
            jumpAmount = data.jumpPower;
        }

        public override void OnUpdate()
        {
            DoAirMove();
            DoJump();
            TransitionState();
        }

        public override void OnExit()
        {
        }

        private void DoAirMove()
        {
            data.rigid.linearVelocityX = data.moveDir.x * data.moveSpeed * Time.deltaTime;
        }

        private void DoJump()
        {
            if (!data.isPressJump || data.jumpState == EJumpState.JumpEnd) return;

            jumpAmount *= data.decelerate;
            data.jumpState = EJumpState.Jumping;
            data.rigid.linearVelocityY = jumpAmount;

            if (data.rigid.linearVelocityY < data.minJumpSpeed)
            {
                data.jumpState = EJumpState.JumpEnd;
                data.isPressJump = false;
                jumpAmount = 0;
                transitionTo((int)EPlayerState.Fall);
            }
        }
        private void TransitionState()
        {
            if (IsFallState()) return;
            if (IsHitState()) return;
        }
        private bool IsFallState()
        {
            if (!data.IsFallState) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }
        private bool IsHitState()
        {
            //TODO : 데미지 상태 추가
            return true;
        }
    }

}
