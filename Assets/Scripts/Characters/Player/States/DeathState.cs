using System;
using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class DeathState : PlayerBaseState
    {
        public DeathState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo)
        {
        }

        public override void OnStart()
        {

            data.playerInfo.animator.SetTrigger(EPlayerAni.DeathTrigger.ToString());
            data.playerInfo.rigid.gravityScale = 0;
            data.playerInfo.rigid.linearVelocity = Vector2.zero;
            data.playerInfo.playerCollider.enabled = false;
        }

        public override void OnUpdate()
        {
            TransitionState();
        }

        public override void OnExit()
        {
            //Death상태를 나간다는 것은 플레이어가 부활했다는 것
            //아마 이 부분에서 플레이어 상태 재설정 해야할 것 같음
            data.playerInfo.playerCollider.enabled = true;
        }
        private void TransitionState()
        {
            if (data.hitInfo.isDeath) return;
            transitionTo((int)EPlayerState.Idle);
        }
    }
}