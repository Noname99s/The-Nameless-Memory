using System;
using Orpul1.State;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class MoveState : PlayerBaseState
    {
        public MoveState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) {}

        public override void OnStart()
        {
        }
        public override void OnUpdate()
        {
            DoMove();
            DoJump();
        }
        public override void OnExit()
        {
            playerContext.rigid.linearVelocity = new Vector2(0, playerContext.rigid.linearVelocityY) ;
        }

        private void DoMove()
        {
            Vector2 movePosi = playerContext.movePosi * playerContext.moveSpeed * Time.deltaTime;
            playerContext.rigid.linearVelocity = new Vector2(movePosi.x, playerContext.rigid.linearVelocityY);
        }
        private void DoJump()
        {
            Debug.Log($"velocty {playerContext.rigid.linearVelocity}");
            if (!playerContext.isPressJump || playerContext.jumpState != EJumpState.JumpReady) return;

            playerContext.rigid.AddForceY(playerContext.jumpPower, ForceMode2D.Impulse);
            if (playerContext.rigid.linearVelocityY > playerContext.maxJumpHeight)
            {
                playerContext.jumpState = EJumpState.JumpEnd;
                playerContext.isPressJump = false;
            }
            
        }
    }

}
