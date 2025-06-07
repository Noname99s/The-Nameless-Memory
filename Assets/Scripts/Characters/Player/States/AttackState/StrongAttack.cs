using Orpul1.Player.Animation;
using Orpul1.Player.Sound;
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
            playerData.playerInfo.animator.SetBool(EPlayerAniTransition.IsStrongAttack.ToString(), true);
            playerData.attackInfo.curAttack = 0;
            aniDuration = 0;
            SetNextAttack();
        }
        public void Execute()
        {
            if (Time.time < aniDuration) return;
            playerData.playerInfo.animator.SetInteger(EPlayerAniTransition.AttackIndex.ToString(), playerData.attackInfo.curAttack);
            if (!playerData.playerInfo.animator.GetCurrentAnimatorStateInfo(0).IsName(((EStrongAttack)(playerData.attackInfo.curAttack)).ToString()))
                return;
            playerData.playerInfo.sound.PlaySound((int)EPlayerSound.StrongAttack);

            aniDuration = playerData.playerInfo.animator.GetCurrentAnimatorStateInfo(0).length;
            SetNextAttack();
        }

        

        private void SetNextAttack()
        {
            playerData.attackInfo.curAttack = Random.Range(0, (int)EStrongAttack.KickC+1);
        }
    }
}