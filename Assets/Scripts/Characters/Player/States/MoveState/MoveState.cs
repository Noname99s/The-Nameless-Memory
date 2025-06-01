using System;
using System.Runtime.CompilerServices;
using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class MoveState : PlayerBaseState
    {
        public MoveState(PlayerContext playerdata, Action<int> transitionTo) : base(playerdata, transitionTo) {}
        bool isFlip = false;
        public override void OnStart()
        {

            data.jumpInfo.jumpCount = 2;
            data.playerInfo.animator.SetBool(EPlayerAni.IsGround.ToString(), true);
            data.moveInfo.slideMovement.gravity = data.moveInfo.slideMoveGravity;
            data.playerInfo.rigid.gravityScale = 0;
            data.playerInfo.rigid.linearVelocityX = 0;
        }
        public override void OnUpdate()
        {
            MoveX();
            data.playerInfo.actionFlipX();
            TransitionState();
        }
        public override void OnExit()
        {

            data.playerInfo.animator.SetFloat(EPlayerAni.MoveSpeed.ToString(), 0);
            data.playerInfo.rigid.gravityScale = data.jumpInfo.gravityStrength;
        }
        private void MoveX()
        {
            float dirX = data.moveInfo.moveDir.x * data.moveInfo.moveSpeed;
            data.moveInfo.slideResult = data.playerInfo.rigid.Slide(Vector2.right * dirX, Time.deltaTime, data.moveInfo.slideMovement);
            data.playerInfo.animator.SetFloat(EPlayerAni.MoveSpeed.ToString(),1);
        }
        private void TransitionState()
        {
            if (IsHitState()) return;
            if (IsDeathState()) return;
            if (IsIdleState()) return;
            if (IsJumpState()) return;
            if (IsFallState()) return;
            if (IsAttackState()) return;
            if (IsClimbLadderState()) return;
        }
        private bool IsHitState()
        {
            if (!data.hitInfo.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }

        private bool IsDeathState()
        {
            if (!data.hitInfo.isDeath) return false;
            transitionTo((int)EPlayerState.Death);

            return true;
        }

        private bool IsJumpState()
        {
            if (!data.IsJumpState) return false;
            transitionTo((int)EPlayerState.Jump);

            return true;
        }
        private bool IsFallState()
        {
            if (!data.fallInfo.isFalling) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }

        private bool IsIdleState()
        {
            if (!data.IsIdleState) return false;
            transitionTo((int)EPlayerState.Idle);
            return true;
        }

        private bool IsAttackState()
        {
            if (!data.attackInfo.isPressAttack) return false;
            transitionTo((int)EPlayerState.Attack);

            return true;
        }

        private bool IsClimbLadderState()
        {
            if (!data.IsClimbLadderState) return false;
            transitionTo((int)EPlayerState.ClimbLadder);

            return true;
        }
    }

}
