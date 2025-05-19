using System;
using Orpul1.State;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class MoveState : PlayerBaseState
    {
        public MoveState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) {}
        float force = 0;
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
            //+ ((force+(0.5f*Time.fixedDeltaTime*-playerContext.gravityStrength))/playerContext.rigid.mass)
        }
        private void DoJump()
        {
           

            if (!playerContext.isPressJump || playerContext.jumpState != EJumpState.JumpReady) return;
            

            playerContext.rigid.AddForceY(playerContext.jumpPower, ForceMode2D.Impulse);
            /*force = playerContext.jumpPower;
            if (playerContext.rigid.linearVelocityY < 0)
                force -= playerContext.rigid.linearVelocityY;*/

            if (playerContext.rigid.linearVelocityY > playerContext.maxJumpHeight)
            {
                playerContext.jumpState = EJumpState.JumpEnd;
                playerContext.isPressJump = false;
            }
            
        }
    }

}
