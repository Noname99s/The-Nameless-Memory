using Orpul1.Player;
using System;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    public JumpState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) {}
    public override void OnStart()
    {
        Debug.Log("JumpStart");
        //playerContext.jumpCollider.enabled = false;
    }
    public override void OnUpdate()
    {
        //if (!playerContext.isPressJump || playerContext.jumpState == EJumpState.Jumping) return;
       /* playerContext.rigid.AddForce(Vector2.up * playerContext.jumpPower, ForceMode2D.Impulse);
        playerContext.jumpState = EJumpState.Jumping;
        playerContext.jumpCollider.enabled = true;
        Debug.Log("JumpUpdate");
        transitionTo((int)EPlayerState.Move);*/
    }

    public override void OnExit()
    {
        Debug.Log("JumpExit");
    }
}
