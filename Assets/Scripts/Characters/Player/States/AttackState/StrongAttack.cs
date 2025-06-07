using Orpul1.Player.Animation;
using Orpul1.Player.Sound;
using Orpul1.State.Attack;
using UnityEngine;

namespace Orpul1.Player.State.Attack
{
    public class StrongAttack : IStrategy
    {
        PlayerContext playerData;
        float attackDuration = 0;
        bool isStartAttackAni = false;
        public StrongAttack(PlayerContext playerData)
        {
            this.playerData = playerData;
        }
        public void Initialize()
        {
            playerData.playerInfo.animator.SetBool(EPlayerAniTransition.IsStrongAttack.ToString(), true);
            playerData.attackInfo.curAttack = 0;
            attackDuration = 0;
            isStartAttackAni = false;

            //SetNextAttack();
        }
        public void Execute()
        {
            playerData.playerInfo.animator.SetInteger(EPlayerAniTransition.AttackIndex.ToString(), playerData.attackInfo.curAttack);
            if (!IsAttackAni()) return;
            if (!IsAttackAniEnd()) return;
            SetNextAttack();
        }

        
        private bool IsAttackAni()
        {
            if (isStartAttackAni)
                return true;
            if (!playerData.playerInfo.animator.GetCurrentAnimatorStateInfo(0).IsName(((EStrongAttack)(playerData.attackInfo.curAttack)).ToString()))
                return false;

            isStartAttackAni = true;
            playerData.playerInfo.sound.PlaySound((int)EPlayerSound.StrongAttack);
            attackDuration = Time.time + playerData.attackInfo.attackDelays[(int)EPlayerAttack.StrongAttack];

            return true;
        }
        private bool IsAttackAniEnd()
        {
            if (Time.time < attackDuration)
                return false;
            isStartAttackAni = false;
            return true;
        }
        private void SetNextAttack()
        {
            playerData.attackInfo.curAttack = Random.Range(0, (int)EStrongAttack.End);
        }
    }
}