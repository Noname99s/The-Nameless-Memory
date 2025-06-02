using System;
using Orpul1.Player.Animation;
using Orpul1.State;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class WallClimb : PlayerBaseState
    {
        float jumpAmount = 0;
        public WallClimb(PlayerContext playerData, Action<int> transitionTo) : base(playerData, transitionTo)
        {
        }

        public override void OnStart()
        {
            data.jumpInfo.jumpCount = 2;
            data.playerInfo.rigid.gravityScale = 0;
            data.playerInfo.rigid.linearVelocity = Vector2.zero;
            data.playerInfo.animator.SetBool(EPlayerAni.IsWallClimb.ToString(), true);
            data.jumpInfo.isPressJump = false;
            data.jumpInfo.shouldWallJump = true;
            jumpAmount = data.jumpInfo.jumpPower;
            Debug.Log("Climb Wall");

        }

        public override void OnUpdate()
        {
            TrainsitionState();
        }

        public override void OnExit()
        {
            data.playerInfo.rigid.gravityScale = data.jumpInfo.gravityStrength;
            data.playerInfo.animator.SetBool(EPlayerAni.IsWallClimb.ToString(), false);
        }
        private void TrainsitionState()
        {
            if (IsHitState()) return;
            if (IsJumpState()) return;
            if (IsFallState()) return;
        }
        private bool IsHitState()
        {
            if (!data.hitInfo.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }
        private bool IsJumpState()
        {
            if (!data.jumpInfo.isPressJump && data.moveInfo.isPressWallClimb) return false;
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