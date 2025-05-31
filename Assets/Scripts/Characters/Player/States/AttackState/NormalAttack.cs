using System;
using UnityEngine;
using Orpul1.Player.Animation;

namespace Orpul1.Player.State.Attack
{
    public class NormalAttack : IStrategy
    {
        PlayerContext playerData;
        float aniDuration = 0;
        public NormalAttack(PlayerContext playerData)
        {
            this.playerData = playerData;
        }
        public void Execute()
        {
            if (Time.time < aniDuration) return;
            playerData.playerInfo.animator.SetInteger(EPlayerAni.AttackIndex.ToString(), playerData.attackInfo.curAttack);
            if (!playerData.playerInfo.animator.GetCurrentAnimatorStateInfo(0).IsName(((ENormalAttack)(playerData.attackInfo.curAttack)).ToString()))
                return;
            aniDuration = playerData.playerInfo.animator.GetCurrentAnimatorStateInfo(0).length;
            
            SetNextAttack();
        }

        public void Initialize()
        {
            playerData.playerInfo.animator.SetBool(EPlayerAni.IsStrongAttack.ToString(), false);
            playerData.attackInfo.curAttack = 0;
        }

        private void SetNextAttack()
        {
            playerData.attackInfo.curAttack++;

            if (playerData.attackInfo.curAttack == (int)ENormalAttack.END)
            {
                playerData.attackInfo.curAttack = (int)ENormalAttack.PunchA;
            }
        }
    }
}