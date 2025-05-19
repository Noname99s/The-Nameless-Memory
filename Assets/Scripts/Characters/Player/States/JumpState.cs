using Orpul1.Player;
using System;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    float jumpAmount = 0;

    public JumpState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) { }
    public override void OnStart()
    {
    }

    public override void OnUpdate()
    {
        if (!data.isPressJump || data.jumpState != EJumpState.JumpReady) return;

        jumpAmount += data.jumpPower;
        if (jumpAmount > data.maxJumpHeight)
        {
            data.jumpState = EJumpState.JumpEnd;
            data.isPressJump = false;
            jumpAmount = 0;
            transitionTo((int)EPlayerState.Fall);
        }
    }

    public override void OnExit()
    {
    }

    
}
