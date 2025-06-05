using System;
using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class WallClimb : PlayerBaseState
    {
        float slideSpeed = 0;
        public WallClimb(PlayerContext playerData, Action<int> transitionTo) : base(playerData, transitionTo)
        {
        }

        public override void OnStart()
        {
            data.jumpInfo.jumpCount = 2;
            data.playerInfo.rigid.gravityScale = 0;
            data.playerInfo.rigid.linearVelocity = Vector2.zero;
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsWallClimb.ToString(), true);
            data.jumpInfo.isPressJump = false;
            data.jumpInfo.shouldWallJump = true;
            slideSpeed = 0;
            Debug.Log("Climb Wall");

        }

        public override void OnUpdate()
        {
            SlideWall();
            TrainsitionState();
        }

        public override void OnExit()
        {
            data.playerInfo.rigid.gravityScale = data.jumpInfo.gravityStrength;
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsWallClimb.ToString(), false);
            data.jumpInfo.jumpCount = 0;
        }
        private void TrainsitionState()
        {
            if (IsHitState()) return;
            if (IsJumpState()) return;
            if (IsFallState()) return;
        }

        private void SlideWall()
        {
            data.playerInfo.rigid.linearVelocityY -= slideSpeed;
            slideSpeed += Time.deltaTime;
        }

        private bool IsHitState()
        {
            if (!data.hitInfo.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }
        private bool IsJumpState()
        {
            if (!data.jumpInfo.isPressJump) return false;
            //data.playerInfo.rigid.linearVelocityX = data.moveInfo.moveDir.x * data.moveInfo.moveSpeed * Time.deltaTime;
            data.moveInfo.isPressWallClimb = false;
            transitionTo((int)EPlayerState.Jump);

            return true;
        }
        private bool IsFallState()
        {
            if(data.IsWallClimbState) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }

    }
}