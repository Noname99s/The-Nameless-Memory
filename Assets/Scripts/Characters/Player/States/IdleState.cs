using UnityEngine;
using System;
using Orpul1.Player.Animation;
namespace Orpul1.Player.State
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) {}

        public override void OnStart()
        {
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
            if (IsJumpState()) return;
            if (IsMoveState()) return;
            if (IsFallState()) return;
            if (IsAttackState()) return;
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
      
    }
}
