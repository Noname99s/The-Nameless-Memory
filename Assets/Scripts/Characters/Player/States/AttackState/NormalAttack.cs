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
            playerData.animator.SetInteger(EPlayerAni.AttackIndex.ToString(), playerData.attackContext.curAttack);
            if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName(((ENormalAttack)(playerData.attackContext.curAttack)).ToString()))
                return;
            aniDuration = playerData.animator.GetCurrentAnimatorStateInfo(0).length;
            
            SetNextAttack();
        }

        public void Initialize()
        {
            playerData.animator.SetBool(EPlayerAni.IsStrongAttack.ToString(), false);
            playerData.attackContext.curAttack = 0;
        }

        private void SetNextAttack()
        {
            playerData.attackContext.curAttack++;

            if (playerData.attackContext.curAttack == (int)ENormalAttack.END)
            {
                playerData.attackContext.curAttack = (int)ENormalAttack.PunchA;
            }
        }
    }
}