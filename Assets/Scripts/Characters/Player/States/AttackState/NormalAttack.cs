using System;
using UnityEngine;
using Orpul1.Player.Animation;

namespace Orpul1.Player.State.Attack
{
    public class NormalAttack : ICommand
    {
        PlayerContext playerData;
        float interval = 0;
        public NormalAttack(PlayerContext playerData)
        {
            this.playerData = playerData;
        }
        public void Execute()
        {
            playerData.animator.SetInteger(EPlayerAni.NormalAttackIndex.ToString(), playerData.attackContext.curAttack);
            if (!IsAttackAni()) 
                return;
            SetNextAttack();

        }
        private bool IsAttackAni()
        {
            if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName(((ENormalAttack)playerData.attackContext.curAttack).ToString()))
                return false;
            return true;
        }
        private void SetNextAttack()
        {
            playerData.attackContext.curAttack++;
            playerData.canAttack = false;

            if (playerData.attackContext.curAttack == (int)ENormalAttack.END)
                playerData.attackContext.curAttack = (int)ENormalAttack.PunchA;

        }
    }
}