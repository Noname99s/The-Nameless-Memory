using Orpul1.Player;
using Orpul1.Player.Animation;
using Orpul1.State.Attack;
using System;
using System.Runtime.CompilerServices;
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
            Debug.Log("Fall Start");

            interval = 0;
            data.fallInfo.fallDamage = 0;
            data.playerInfo.rigid.linearVelocityY = -data.fallInfo.defaultFallSpeed;
            data.jumpInfo.shouldWallJump = false;
            data.playerInfo.animator.SetBool(EPlayerAni.IsJump.ToString(), false);
            data.playerInfo.animator.SetBool(EPlayerAni.IsGround.ToString(), false);
            maxMoveX = Mathf.Max(data.playerInfo.rigid.linearVelocityX, data.fallInfo.onAirMoveSpeed * Time.deltaTime);

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
            data.playerInfo.animator.SetBool(EPlayerAni.IsGround.ToString(), true);
            data.attackInfo.attackType = EPlayerAttack.NONE;
        }

        private void AirMove()
        {
            if(data.fallInfo.isFalling)
                data.playerInfo.rigid.linearVelocityY = Mathf.Clamp(data.playerInfo.rigid.linearVelocityY, -data.fallInfo.maxFallSpeed, 0);
            moveX = data.playerInfo.rigid.linearVelocityX + data.moveInfo.moveDir.x * data.fallInfo.onAirMoveSpeed * Time.deltaTime;
            data.playerInfo.rigid.linearVelocityX = Mathf.Clamp(moveX, -maxMoveX, maxMoveX);
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
            interval += Time.deltaTime;
            if (interval < data.fallInfo.fallDamageOverTime) return false;
            return true;
        }
        private void TransitionState()
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
            if (!data.playerInfo.isHit) return false;
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
            if (!data.playerInfo.isDeath) return false;
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
