using System;
using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class ClimbLadderState : PlayerBaseState
    {
        public ClimbLadderState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo)
        {
        }

        public override void OnStart()
        {
            data.rigid.gravityScale = 0;
            data.jumpCount = 2;
            data.animator.SetBool(EPlayerAni.IsClimbLadder.ToString(), true);
        }

        public override void OnUpdate()
        {
            data.rigid.linearVelocityY = data.moveDir.y * data.climbSpeed * Time.deltaTime;
            data.rigid.linearVelocityX = data.moveDir.x * data.moveSpeed * Time.deltaTime;
          
            data.animator.speed = Mathf.Abs(data.moveDir.y);

            TransitionState();
        }

        public override void OnExit()
        {
            data.rigid.gravityScale = data.gravityStrength;
            data.animator.speed = 1;
            data.animator.SetBool(EPlayerAni.IsClimbLadder.ToString(), false);
        }
        
        
        private void TransitionState()
        {
            if (IsHitState()) return;
            if (IsJumpState()) return;
            if (IsIdleState()) return;
            if (IsFallState()) return;
        }
        
        private bool IsHitState()
        {
            if (!data.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }
        private bool IsFallState()
        {
            if (data.canClimbLadder || !data.isFalling) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }
        private bool IsIdleState()
        {
            if (data.IsClimbLadderState) return false;
            if (!data.IsIdleState || data.isFalling) return false;
            transitionTo((int)EPlayerState.Idle);
            Debug.Log("LadderClimb->Idle");

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