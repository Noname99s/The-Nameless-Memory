using System;
using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class DeathState : PlayerBaseState
    {
        bool isStartDieAni = false;
        float aniLength = 0;
        public DeathState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo)
        {
        }

        public override void OnStart()
        {
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsDie.ToString(), true);
            data.playerInfo.animator.SetTrigger(EPlayerAniTransition.DeathTrigger.ToString());
            data.playerInfo.rigid.gravityScale = 0;
            data.playerInfo.rigid.linearVelocity = Vector2.zero;
            data.playerInfo.playerCollider.enabled = false;
            aniLength = 0;
            isStartDieAni = false;
        }

        public override void OnUpdate()
        {
            if (!IsDeathAni()) return;
            if (!IsDeathAniEnd()) return;

            // >> : Death 애니메이션 끝난 시점

            Debug.Log("DeathAni End!");
            TransitionState();
        }

        public override void OnExit()
        {
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsDie.ToString(), false);
            data.playerInfo.playerCollider.enabled = true;
        }
        private bool IsDeathAni()
        {
            if(isStartDieAni) return true;
            if (!data.playerInfo.animator.GetCurrentAnimatorStateInfo(0).IsName(EPlayerAni.Die.ToString()))
                return false;
            isStartDieAni = true;
            aniLength = Time.time + data.playerInfo.animator.GetCurrentAnimatorStateInfo(0).length;
            return true;
        }
        private bool IsDeathAniEnd()
        {
            if (Time.time < aniLength) 
                return false;
            return true;
        }
        private void TransitionState()
        {
            if (data.hitInfo.isDeath) return;
            transitionTo((int)EPlayerState.Idle);
        }
    }
}