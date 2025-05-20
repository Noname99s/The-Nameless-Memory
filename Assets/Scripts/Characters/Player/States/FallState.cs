using Orpul1.Player;
using System;
using UnityEngine;

public class FallState : PlayerBaseState
{
    public FallState(PlayerContext data, Action<int> transitionTo) : base(data, transitionTo)
    {
    }
    public override void OnStart()
    {
        data.fallDamage = 0;
        data.rigid.linearVelocityY = -data.defaultFallSpeed;
    }

    public override void OnUpdate() 
    {
        AirMove();
        SetFallDamage();
        TransitionState();
    }

    public override void OnExit() { }

    private void AirMove()
    {
        data.rigid.linearVelocityY = Mathf.Clamp(data.rigid.linearVelocityY, -data.maxFallSpeed, 0);
        data.rigid.linearVelocityX = data.moveDir.x * data.moveSpeed * Time.deltaTime;
    }
    private void SetFallDamage()
    {
        if (!IsToHeigh()) return;
        data.fallDamage = data.maxFallDamage;
    }

    private bool IsToHeigh()
    {
        if (data.rigid.linearVelocityY > -data.damageFallHeight) return false;
        return true;
    }
    private void TransitionState()
    {
        if (IsIdleState()) return;
    }
    private bool IsIdleState()
    {
        if (data.IsFallState) return false;
        transitionTo((int)EPlayerState.Idle);

        return true;
    }
}
