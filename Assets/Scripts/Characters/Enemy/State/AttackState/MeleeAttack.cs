using Orpul1.Enemy.Weapon;
using System;
using UnityEngine;

namespace Orpul1.Enemy.State.Attack
{
    public class MeleeAttack : IStrategy
    {
        EnemyContext context;
        Action<int> transitionTo;
        bool isAttackEnd = true;
        float attackStateEnterTime = 0;
        public MeleeAttack(EnemyContext context, Action<int> transitionTo)
        {
            this.context = context;
            this.transitionTo = transitionTo;
        }
        public void Initialize()
        {
            AbleAttack();
            DoAttack();
        }

        public void Execute()
        {
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            if (attackStateEnterTime > Time.time) return;
            transitionTo((int)EEnemyState.Idle);
        }
        private void AbleAttack()
        {
            if (attackStateEnterTime < Time.time)
            {
                attackStateEnterTime = 0;
                isAttackEnd = true;
            }
            else
            {
                context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), 0);
                context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), false);
            }
        }
        private void DoAttack()
        {
            if (!isAttackEnd) return;
            FlipX();

            context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), 0);
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), true);
            attackStateEnterTime = Time.time + context.attackInfo.attackDelay;
            isAttackEnd = false;
        }
        private void FlipX()
        {
            Vector2 dir = context.chaseInfo.chaseTarget.position - context.transform.position;
            context.enemyInfo.flipX(dir.normalized.x);
        }
    }
}