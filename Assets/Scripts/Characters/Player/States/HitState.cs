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
            TransitionState();
        }

        public override void OnExit()
        {
            data.hitInfo.isHit = false;
        }
        private void TransitionState()
        {
            if (IsIdleState()) return;
            if (IsFallState()) return;
            if (IsMoveState()) return;
        }
        private bool IsIdleState()
        {
            if(!data.IsIdleState) return false;
            transitionTo((int)EPlayerState.Idle);

            return true;
        }
        private bool IsFallState()
        {
            if (!data.IsFallState) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }
        private bool IsMoveState()
        {
            if (!data.IsMoveState) return false;
            transitionTo((int)EPlayerState.Move);

            return true;
        }
    }
}