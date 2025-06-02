using Orpul1.Enemy.State.Attack;
using System;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class AttackState : EnemyState
    {
        IStrategy[] attackStrategies ;
        float interval = 0;
        public AttackState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
            attackStrategies = new IStrategy[2]
            {
                new MeleeAttack(context),
                new RangedAttack(context)
            };
        }

        public override void OnStart()
        {
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), true);
            interval = 0;
        }

        public override void OnUpdate()
        {
            TransitionState();
            if (!IsAttackAni()) return;
            interval += Time.deltaTime;

            if (!IsAttackAniEnd()) return;
            transitionTo((int)EEnemyState.Idle);
        }

        public override void OnExit()
        {
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), false);
        }

        private bool IsAttackAni()
        {
            if (!context.enemyInfo.animator.GetCurrentAnimatorStateInfo(0).IsName(EEnemyAni.Attack.ToString()))
                return false;
            return true;
        }
        private bool IsAttackAniEnd()
        {
            if (interval < context.enemyInfo.animator.GetCurrentAnimatorStateInfo(0).length)
                return false;
            return true;
        }
        protected override void TransitionState()
        {
            if (IsPatrolState()) return;
        }
        private bool IsPatrolState()
        {
            if (!context.IsPatrolState) return false;
            transitionTo((int)EEnemyState.Patrol);

            return true;
        }
    }
}