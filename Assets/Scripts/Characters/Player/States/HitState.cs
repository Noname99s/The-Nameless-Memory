using System;
using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Player.State.Hit
{
    public class HitState : PlayerBaseState
    {
        float hitTime = 0;
        public HitState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo)
        {
        }
        public override void OnStart()
        {
            data.playerInfo.animator.SetTrigger(EPlayerAni.HitTrigger.ToString());
            hitTime = Time.time + data.hitInfo.hitDuration;
        }
        
        public override void OnUpdate()
        {
            if (Time.time < hitTime) return;
            transitionTo((int)EPlayerState.Idle);
        }

        public override void OnExit()
        {
            data.hitInfo.isHit = false;
        }
    }
}