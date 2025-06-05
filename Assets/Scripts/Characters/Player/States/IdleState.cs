using UnityEngine;
using System;
using Orpul1.Player.Animation;
using Orpul1.State.Attack;
namespace Orpul1.Player.State
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) {}

        public override void OnStart()
        {

            data.jumpInfo.jumpCount = 2;
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsIdle.ToString(), true);
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsGround.ToString(), true);
            data.playerInfo.rigid.linearVelocity = Vector2.zero;
            data.playerInfo.rigid.gravityScale = 0;
            data.playerInfo.trPlayer.position = data.playerInfo.dectectedGroundPosi;
        }
        public override void OnUpdate()
        {
            data.playerInfo.rigid.linearVelocityX = 0;
            TransitionState();
        }
        public override void OnExit()
        {
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsIdle.ToString(), false);
            data.playerInfo.rigid.gravityScale = 5;
        }

        private void TransitionState()
        {
            if (IsHitState()) return;
            if (IsDeathState()) return;
            if (IsJumpState()) return;
            if (IsMoveState()) return;
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
            if (!data.IsDieState) return false;
            transitionTo((int)EPlayerState.Death);

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
        private bool IsFallState()
        {
            if (!data.fallInfo.isFalling) return false;
            transitionTo((int)EPlayerState.Fall);

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
