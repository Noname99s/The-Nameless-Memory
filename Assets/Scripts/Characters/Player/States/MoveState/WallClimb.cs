using System;
using Orpul1.State;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class WallClimb : PlayerBaseState
    {
        public WallClimb(PlayerContext playerData, Action<int> transitionTo) : base(playerData, transitionTo)
        {
        }

        public override void OnStart()
        {
            data.jumpCount = 2;
            data.rigid.gravityScale = 0;
            data.rigid.linearVelocity = Vector2.zero;
        }

        public override void OnUpdate()
        {
            TrainsitionState();
        }

        public override void OnExit()
        {
            data.rigid.gravityScale = data.gravityStrength;
        }
        private void TrainsitionState()
        {
            if (IsJumpState()) return;
            //if (IsFallState()) return;
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
            if(!data.IsFallState) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }

    }
}