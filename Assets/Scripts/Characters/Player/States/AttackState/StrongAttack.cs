using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Player.State.Attack
{
    public class StrongAttack : IStrategy
    {
        PlayerContext playerData;
        float aniDuration = 0;
        public StrongAttack(PlayerContext playerData)
        {
            this.playerData = playerData;
        }

        public void Execute()
        {
            if (Time.time < aniDuration) return;
            playerData.animator.SetInteger(EPlayerAni.NormalAttackIndex.ToString(), playerData.attackContext.curAttack);
            if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName(((ENormalAttack)(playerData.attackContext.curAttack)).ToString()))
                return;
            aniDuration = playerData.animator.GetCurrentAnimatorStateInfo(0).length;
            SetNextAttack();
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