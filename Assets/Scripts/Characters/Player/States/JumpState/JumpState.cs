using Orpul1.Player;
using Orpul1.Player.Animation;
using Orpul1.State.Attack;
using System;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class JumpState : PlayerBaseState
    {
        float jumpAmount = 0;
        float moveX = 0;
        float maxMoveX = 0;
        bool isFlip = false;
        public JumpState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) { }
        public override void OnStart()
        {
            data.animator.SetBool(EPlayerAni.IsGround.ToString(), false);
            data.animator.SetBool(EPlayerAni.IsJump.ToString(),true);
            jumpAmount = data.jumpPower;
            maxMoveX = data.moveSpeed * Time.deltaTime;
        }

        public override void OnUpdate()
        {
            FlipX();
            AirMove();
            DoJump();
            TransitionState();
        }

        public override void OnExit()
        {
            data.animator.SetBool(EPlayerAni.IsJump.ToString(), false);
        }

        
        private void AirMove()
        {
            moveX = data.rigid.linearVelocityX + data.moveDir.x * data.onAirMoveSpeed * Time.deltaTime;
            data.rigid.linearVelocityX = Mathf.Clamp(moveX, -maxMoveX, maxMoveX);
        }
        private void FlipX()
        {
            if (data.moveDir.x < 0)
                isFlip = true;
            else if (data.moveDir.x > 0)
                isFlip = false;
            data.sprite.flipX = isFlip;
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
