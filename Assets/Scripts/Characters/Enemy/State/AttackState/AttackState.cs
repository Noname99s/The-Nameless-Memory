using Orpul1.Enemy.State.Attack;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            context.enemyInfo.rigid.gravityScale = context.enemyInfo.gravityStrength;
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), true);
            interval = 0;
        }

        public override void OnUpdate()
        {
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            if (IsHitState()) return;
            if (!IsAttackAni()) return;
            interval += Time.deltaTime;

            if (!IsAttackAniEnd()) return;
            TransitionState();
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
            transitionTo((int)EEnemyState.Chase);
        }
        private bool IsChaseState()
        {
            if (!context.IsChaseState) return false;
            transitionTo((int)EEnemyState.Chase);

            return true;
        }
        private bool IsPatrolState()
        {
            if (!context.IsPatrolState) return false;
            transitionTo((int)EEnemyState.Patrol);

            return true;
        }
        private bool IsHitState()
        {
            if (!context.IsHitState) return false;
            transitionTo((int)EEnemyState.Hit);

            return true;
        }
    }
}