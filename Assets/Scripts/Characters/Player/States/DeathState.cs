using System;
using Orpul1.Player.Animation;
using Orpul1.Player.Sound;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class DeathState : PlayerBaseState
    {
        bool isStartDeathAni = false;
        float aniLength = 0;

        event Action<string> OnDeathNotify;
        public DeathState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo)
        {
            OnDeathNotify += Managers.UI.GetPopupUI<InGameComposition>().OnSystemTextUpdateConnection;
        }

        public override void OnStart()
        {
            OnDeathNotify?.Invoke(BG_DB_Dialogtext.GetEntity("Text_UI")._DialogLines[3]._Text);

            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsDie.ToString(), true);
            data.playerInfo.animator.SetTrigger(EPlayerAniTransition.DeathTrigger.ToString());
            data.playerInfo.rigid.gravityScale = 0;
            data.playerInfo.rigid.linearVelocity = Vector2.zero;
            data.playerInfo.playerCollider.enabled = false;
            aniLength = 0;
            isStartDeathAni = false;

            ++Managers.Game.playData.playerStatus.deathCount;
            Managers.Map.RecordDeathInfo(data.playerInfo.dectectedGroundPosi);
            Managers.Map.getFragment = false;
            data.playerInfo.animator.SetTrigger(EPlayerAni.DeathTrigger.ToString());
            data.playerInfo.sound.PlaySound((int)EPlayerSound.Death);
        }

        public override void OnUpdate()
        {
            if (!IsDeathAni()) return;
            if (!IsDeathAniEnd()) return;

            // >> : Death 애니메이션 끝난 시점
            Managers.Map.RespawnPlayer();

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
            if(isStartDeathAni) return true;
            if (!data.playerInfo.animator.GetCurrentAnimatorStateInfo(0).IsName(EPlayerAni.Die.ToString()))
                return false;
            isStartDeathAni = true;
            aniLength = Time.time + data.playerInfo.animator.GetCurrentAnimatorStateInfo(0).length;
            return true;
        }
        private bool IsDeathAniEnd()
        {
            if (Time.time < aniLength) 
                return false;
            return true;
        }
        protected override void TransitionState()
        {
            if (data.hitInfo.isDeath) return;
            transitionTo((int)EPlayerState.Idle);
        }
    }
}