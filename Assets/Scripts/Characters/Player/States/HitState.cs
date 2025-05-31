using System;
using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Player.State.Hit
{
    public class HitState : PlayerBaseState
    {
        bool isHitAni = false;
        float aniDuration = 0;
        float interval = 0;
        public HitState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo)
        {
        }
        public override void OnStart()
        {

            data.playerInfo.animator.SetTrigger(EPlayerAni.HitTrigger.ToString());
            isHitAni = false;
        }
        
        public override void OnUpdate()
        {
            if (!isHitAni)
            {
                SetHitAniDuration();
            }
            else
            {
                WaitForHitAniEnd();
            }
        }

        public override void OnExit()
        {
            data.playerInfo.isHit = false;
        }
        private void SetHitAniDuration()
        {
            if (!data.playerInfo.animator.GetCurrentAnimatorStateInfo(0).IsName("HitDamage")) return;
            aniDuration = data.playerInfo.animator.GetCurrentAnimatorStateInfo(0).length;
            isHitAni = true;
        }
        private void WaitForHitAniEnd()
        {
            interval += Time.deltaTime;
            if (interval < aniDuration) return;
            transitionTo((int)EPlayerState.Idle);
        }
    }
}