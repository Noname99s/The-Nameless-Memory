using Orpul1.Player;
using Orpul1.Player.Animation;
using Orpul1.Player.State.Jump;
using Orpul1.State.Attack;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Orpul1.Player.State
{
    
    public class JumpState : PlayerBaseState
    {
        
        float moveX = 0;
        float maxMoveX = 0;
        bool isFlip = false;
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
            jumpStraties[(int)jumpType].Initialize();
            data.animator.SetBool(EPlayerAni.IsJump.ToString(),true);
            maxMoveX = data.moveSpeed * Time.deltaTime;
        }

        public override void OnUpdate()
        {
            FlipX();
            AirMove();
            jumpStraties[(int)jumpType].Execute();
            TransitionState();
        }

        public override void OnExit()
        {
            data.animator.SetBool(EPlayerAni.IsJump.ToString(), false);
            data.shouldWallJump = false;
        }

        
        private void SetJumpType()
        {
            if(data.shouldWallJump)
                jumpType = EPlayerJump.WallJump;
            else
                jumpType = EPlayerJump.NormalJump;
        }
        private void AirMove()
        {
            moveX = data.rigid.linearVelocityX + data.moveDir.x * data.onAirMoveSpeed * Time.deltaTime;
            data.rigid.linearVelocityX = Mathf.Clamp(moveX, -maxMoveX, maxMoveX);
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
            if (IsWallClimbState()) return;
            if (IsFallState()) return;
            if (IsHitState()) return;
        }
        private bool IsFallState()
        {
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
        private bool IsHitState()
        {
            //TODO : 데미지 상태 추가
            return true;
        }
    }

}
