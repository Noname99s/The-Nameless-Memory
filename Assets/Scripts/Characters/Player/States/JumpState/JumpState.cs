using Orpul1.Player.Animation;
using Orpul1.Player.State.Jump;
using System;
using UnityEngine;

namespace Orpul1.Player.State
{
    
    public class JumpState : PlayerBaseState
    {
        
        float moveX = 0;
        float maxMoveX = 0;
        IStrategy[] jumpStraties = new IStrategy[2];
        EPlayerJump jumpType = EPlayerJump.NormalJump;
        public JumpState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) 
        {
            jumpStraties[(int)EPlayerJump.NormalJump] = new NormalJump(playerContext);
            jumpStraties[(int)EPlayerJump.WallJump] = new WallJump(playerContext);
        }
        public override void OnStart()
        {
            SetJumpType();
            Debug.Log("JumpState Start");

            jumpStraties[(int)jumpType].Initialize();
            data.playerInfo.animator.SetBool(EPlayerAni.IsJump.ToString(),true);
            data.moveInfo.isPressWallClimb = false;
            //maxMoveX = data.moveInfo.moveSpeed * Time.deltaTime;
        }

        public override void OnUpdate()
        {
            data.playerInfo.actionFlipX();
            AirMove();
            jumpStraties[(int)jumpType].Execute();
            TransitionState();
        }

        public override void OnExit()
        {
            data.playerInfo.animator.SetBool(EPlayerAni.IsJump.ToString(), false);
            data.jumpInfo.shouldWallJump = false;
        }

        
        private void SetJumpType()
        {
            if(data.jumpInfo.shouldWallJump)
                jumpType = EPlayerJump.WallJump;
            else
                jumpType = EPlayerJump.NormalJump;
        }
        private void AirMove()
        {
            moveX = data.moveInfo.moveDir.x * data.fallInfo.onAirMoveSpeed;
            data.playerInfo.rigid.linearVelocityX = moveX;
        }
        private void TransitionState()
        {
            if (IsIdleState()) return;
            if (IsMoveState()) return;
            if (IsHitState()) return;
            if (IsDeathState()) return;
            if (IsClimbLadderState()) return;
            if (IsWallClimbState()) return;
            if (IsFallState()) return;
        }
        private bool IsIdleState()
        {
            if (data.jumpInfo.jumpState != EJumpState.JumpEnd) return false;    
            if (!data.IsIdleState) return false;
            transitionTo((int)EPlayerState.Idle);

            return true;
        }
        private bool IsMoveState()
        {
            if (data.fallInfo.isFalling || !data.IsMoveState|| data.jumpInfo.jumpState != EJumpState.JumpReady) return false;
            transitionTo((int)EPlayerState.Move);

            return true;
        }
        private bool IsHitState()
        {
            if (!data.playerInfo.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }

        private bool IsDeathState()
        {
            if (!data.playerInfo.isDeath) return false;
            transitionTo((int)EPlayerState.Death);

            return true;
        }

        private bool IsFallState()
        {

            if (data.jumpInfo.jumpState != EJumpState.JumpEnd) return false;
            if (!data.IsFallState) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }
        private bool IsWallClimbState()
        {
            if (!data.IsWallClimbState) return false;
            transitionTo((int)EPlayerState.WallClimb);

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
