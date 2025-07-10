using Orpul1.Player.Animation;
using Orpul1.State.Attack;
using System;
using UnityEngine;


namespace Orpul1.Player.State
{
    public class FallState : PlayerBaseState
    {
        float fallInterval = 0;
        float moveX = 0;
        public FallState(PlayerContext data, Action<int> transitionTo) : base(data, transitionTo) { }
        public override void OnStart()
        {

            fallInterval = 0;
            data.fallInfo.fallDamage = 0;
            data.playerInfo.rigid.linearVelocityY = -data.fallInfo.defaultFallSpeed;
            data.jumpInfo.shouldWallJump = false;
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsGround.ToString(), false);
        }

        public override void OnUpdate()
        {
            AirMove();
            data.playerInfo.actionFlipX();
            SetFallDamage();
            TransitionState();
        }

        public override void OnExit() 
        {
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsGround.ToString(), true);
            data.attackInfo.attackType = EPlayerAttack.NONE;
        }

        private void AirMove()
        {
            moveX = data.moveInfo.moveDir.x * data.fallInfo.onAirMoveSpeed;
            data.playerInfo.rigid.linearVelocityX = moveX;
        }


        private void SetFallDamage()
        {
            if (!IsToHeigh()) return;
            data.fallInfo.fallDamage = data.fallInfo.maxFallDamage;
        }

        private bool IsToHeigh()
        {
            float dot = Vector2.Dot(data.transform.position, Vector2.down);
            if (dot > 0) return false;
            fallInterval += Time.deltaTime;
            if (fallInterval < data.fallInfo.fallDamageOverTime) return false;
            return true;
        }
        protected override void TransitionState()
        {
            if (IsHitState()) return;
            if (IsDeathState()) return;
            if (IsWallClimbState()) return;
            if (IsClimbLadderState()) return;
            if (IsIdleState()) return;
            if (IsJumpState()) return;
            if (IsMoveState()) return;
        }
        private bool IsHitState()
        {
            if (!data.hitInfo.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }
        private bool IsClimbLadderState()
        {
            if (!data.IsClimbLadderState) return false;
            transitionTo((int)EPlayerState.ClimbLadder);

            return true;
        }
        private bool IsDeathState()
        {
            if (!data.IsDieState) return false;
            transitionTo((int)EPlayerState.Death);

            return true;
        }

        private bool IsWallClimbState()
        {
            if (!data.IsWallClimbState) return false;
            transitionTo((int)EPlayerState.WallClimb);

            return true;
        }

        private bool IsIdleState()
        {
            if (!data.IsIdleState) return false;
            transitionTo((int)EPlayerState.Idle);

            return true;
        }
        private bool IsJumpState()
        {
            if (!data.IsJumpState) return false;
            transitionTo((int)EPlayerState.Jump);

            return true;
        }
        private bool IsMoveState()
        {
            if (!data.IsMoveState) return false;
            transitionTo((int)EPlayerState.Move);

            return true;
        }
    }

}
