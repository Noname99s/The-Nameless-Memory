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
            Debug.Log("ClimbLadderState Start");
            data.playerInfo.rigid.gravityScale = 0;
            data.jumpInfo.jumpCount = 2;
            data.playerInfo.animator.SetBool(EPlayerAni.IsClimbLadder.ToString(), true);
            data.playerInfo.playerCollider.isTrigger = true;
        }

        public override void OnUpdate()
        {
            data.playerInfo.rigid.linearVelocityY = data.moveInfo.moveDir.y * data.moveInfo.climbSpeed;
            //data.playerInfo.rigid.linearVelocityX = data.moveInfo.moveDir.x * data.moveInfo.moveSpeed;
          
            data.playerInfo.animator.speed = Mathf.Abs(data.moveInfo.moveDir.y);

            TransitionState();
        }

        public override void OnExit()
        {
            data.playerInfo.rigid.gravityScale = data.jumpInfo.gravityStrength;
            data.playerInfo.animator.speed = 1;
            data.playerInfo.animator.SetBool(EPlayerAni.IsClimbLadder.ToString(), false);
            data.playerInfo.playerCollider.isTrigger = false;
        }
        
        
        private void TransitionState()
        {
            if (IsHitState()) return;
            //if (IsJumpState()) return;
            if (IsIdleState()) return;
            if (IsFallState()) return;
        }
        
        private bool IsHitState()
        {
            if (!data.hitInfo.isHit) return false;
            data.moveInfo.canClimbLadder = false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }
        private bool IsFallState()
        {
            if (data.moveInfo.canClimbLadder || !data.fallInfo.isFalling) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }
        private bool IsIdleState()
        {
            if (data.moveInfo.canClimbLadder) return false;
            if (!data.IsIdleState || data.fallInfo.isFalling) return false;
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