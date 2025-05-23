using Orpul1.Player;
using Orpul1.Player.Animation;
using Orpul1.State.Attack;
using System;
using UnityEngine;


namespace Orpul1.Player.State
{
    public class FallState : PlayerBaseState
    {
        float interval = 0;
        float moveX = 0;
        float maxMoveX = 0;
        bool isFlip = false;
        public FallState(PlayerContext data, Action<int> transitionTo) : base(data, transitionTo)
        {
        }
        public override void OnStart()
        {
            interval = 0;
            data.fallDamage = 0;
            data.rigid.linearVelocityY = -data.defaultFallSpeed;
            data.shouldWallJump = false;
            data.animator.SetBool(EPlayerAni.IsGround.ToString(), false);
            data.animator.SetBool(EPlayerAni.IsJump.ToString(), false);
            maxMoveX = data.rigid.linearVelocityX < 0 ? -data.rigid.linearVelocityX : data.rigid.linearVelocityX;
        }

        public override void OnUpdate()
        {
            AirMove();
            FlipX();
            SetFallDamage();
            TransitionState();
        }

        public override void OnExit() 
        {
            data.animator.SetBool(EPlayerAni.IsGround.ToString(), true);
            data.attackContext.attackType = EPlayerAttack.NONE;
        }

        private void AirMove()
        {
            if(data.isFalling)
                data.rigid.linearVelocityY = Mathf.Clamp(data.rigid.linearVelocityY, -data.maxFallSpeed, 0);
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

        private void SetFallDamage()
        {
            if (!IsToHeigh()) return;
            data.fallDamage = data.maxFallDamage;
        }

        private bool IsToHeigh()
        {
            float dot = Vector2.Dot(data.transform.position, Vector2.down);
            if (dot > 0) return false;
            interval += Time.deltaTime;
            if (interval < data.fallDamageOverTime) return false;
            return true;
        }
        private void TransitionState()
        {
            //if (IsMoveState()) return;
            if (IsWallClimbState()) return;
            if (IsIdleState()) return;
            if (IsJumpState()) return;
        }

        private bool IsWallClimbState()
        {
            if (!data.IsWallClimbState) return false;
            transitionTo((int)EPlayerState.WallClimb);

            return true;
        }

        private bool IsIdleState()
        {
            if (data.IsFallState) return false;
            transitionTo((int)EPlayerState.Idle);

            return true;
        }
        private bool IsJumpState()
        {
            if (!data.IsJumpState) return false;
            transitionTo((int)EPlayerState.Jump);

            return true;
        }
    }

}
