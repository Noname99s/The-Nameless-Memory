using System;
using System.Data.Common;
using Orpul1.Player.Animation;
using Orpul1.State;
using UnityEditor.Build;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class MoveState : PlayerBaseState
    {
        public MoveState(PlayerContext playerdata, Action<int> transitionTo) : base(playerdata, transitionTo) {}
        bool isFlip = false;
        public override void OnStart()
        {
            data.jumpCount = 2;
        }
        public override void OnUpdate()
        {
            MoveX();
            FlipX();
            TransitionState();
        }
        public override void OnExit()
        {
            data.rigid.linearVelocity = new Vector2(0, data.rigid.linearVelocityY) ;
            data.animator.SetFloat(EPlayerAni.MoveSpeed.ToString(), 0);
        }
        private void MoveX()
        {
            float dirX = data.moveDir.x * data.moveSpeed * Time.deltaTime;
            data.slideResult = data.rigid.Slide(Vector2.right * dirX, Time.deltaTime, data.slideMovement);
            data.animator.SetFloat(EPlayerAni.MoveSpeed.ToString(),1);
        }
        private void FlipX()
        {
            if (data.moveDir.x < 0) 
                isFlip = true;
            else if (data.moveDir.x > 0) 
                isFlip = false;
            data.sprite.flipX = isFlip;
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
        private bool IsFallState()
        {
            if (!data.isFalling) return false;
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
