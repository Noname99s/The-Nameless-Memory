using Orpul1.Player;
using System;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    float jumpAmount = 0;

    public JumpState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) { }
    public override void OnStart()
    {
        jumpAmount = 0;
    }

    public override void OnUpdate()
    {
        OnDamage();
        DoAirMove();
        DoJump();
    }

    public override void OnExit()
    {
    }

    private void DoAirMove()
    {
        data.rigid.linearVelocityX = data.movePosi.x * data.moveSpeed * Time.deltaTime;
    }

    private void DoJump()
    {
        if (!data.isPressJump || data.jumpState != EJumpState.JumpReady) return;

        jumpAmount += data.jumpPower;
        data.jumpState = EJumpState.Jumping;

        data.rigid.linearVelocityY = data.jumpPower;
        if (jumpAmount > data.maxJumpHeight)
        {
            data.jumpState = EJumpState.JumpEnd;
            data.isPressJump = false;
            jumpAmount = 0;
            transitionTo((int)EPlayerState.Fall);
        }
    }
    private void OnDamage()
    {
        //TODO : 데미지 상태 추가
    }
}
