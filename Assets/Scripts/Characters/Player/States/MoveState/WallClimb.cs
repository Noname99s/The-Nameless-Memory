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
            data.jumpCount = 2;
            data.rigid.gravityScale = 0;
            data.rigid.linearVelocity = Vector2.zero;
            data.animator.SetBool(EPlayerAni.IsWallClimb.ToString(), true);
            data.shouldWallJump = true;
            jumpAmount = data.jumpPower;
            Debug.Log("Climb Wall");

        }

        public override void OnUpdate()
        {
            TrainsitionState();
        }

        public override void OnExit()
        {
            data.rigid.gravityScale = data.gravityStrength;
            data.animator.SetBool(EPlayerAni.IsWallClimb.ToString(), false);
        }
        private void TrainsitionState()
        {
            if (IsHitState()) return;
            if (IsJumpState()) return;
            if (IsFallState()) return;
        }
        private bool IsHitState()
        {
            if (!data.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }
        private bool IsJumpState()
        {
            if (!data.isPressJump && data.isPressWallClimb) return false;
            data.rigid.linearVelocityX = data.moveDir.x * data.moveSpeed * Time.deltaTime;
            data.isPressWallClimb = false;
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