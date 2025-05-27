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
        public void Initialize()
        {
            playerData.animator.SetBool(EPlayerAni.IsStrongAttack.ToString(), true);
            SetNextAttack();
        }
        public void Execute()
        {
            if (Time.time < aniDuration) return;
            playerData.animator.SetInteger(EPlayerAni.AttackIndex.ToString(), playerData.attackContext.curAttack);
            if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName(((EStrongAttack)(playerData.attackContext.curAttack)).ToString()))
                return;
            aniDuration = playerData.animator.GetCurrentAnimatorStateInfo(0).length;
            SetNextAttack();
        }

        

        private void SetNextAttack()
        {
            playerData.attackContext.curAttack = Random.Range(0, (int)EStrongAttack.END);
        }
    }
}