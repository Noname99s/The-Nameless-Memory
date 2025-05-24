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
            data.jumpCount = 2;
            //data.animator.SetTrigger(EPlayerAni.IdleTrigger.ToString());
        }
        public override void OnUpdate()
        {
            data.rigid.linearVelocityX = 0;
            TransitionState();
        }
        public override void OnExit()
        {
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
            if (!data.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }

        private bool IsDeathState()
        {
            if (!data.isDeath) return false;
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
            if (!data.isFalling) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }
        private bool IsAttackState()
        {
            if (!data.isPressAttack) return false;
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
