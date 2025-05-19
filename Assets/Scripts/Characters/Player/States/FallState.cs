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
    }

    public override void OnUpdate() 
    {
        OnFloor();
        FallingMove();
        if (!IsToHeigh()) return;
        data.fallDamage = data.maxFallDamage;
    }

    public override void OnExit() { }

    private void FallingMove()
    {
        data.rigid.linearVelocityY = Mathf.Clamp(data.rigid.linearVelocityY, -data.maxFallSpeed, 0);
        data.rigid.linearVelocityX = data.movePosi.x * data.moveSpeed * Time.deltaTime;
    }
    private bool IsToHeigh()
    {
        if (data.rigid.linearVelocityY > -data.damageFallHeight) return false;
        Debug.Log($"velocityY {data.rigid.linearVelocityY}");
        return true;
    }
    private void OnFloor()
    {
        if (data.isFalling) return;
        transitionTo((int)EPlayerState.Idle);
    }
}
