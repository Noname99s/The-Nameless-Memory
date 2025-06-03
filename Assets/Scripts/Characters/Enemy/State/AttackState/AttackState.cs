using Orpul1.Enemy.State.Attack;
using System;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class AttackState : EnemyState
    {
        IStrategy[] attackStrategies ;
        EEnemyAttack attackType;
        float interval = 0;
        bool isAttacking = false;
        public AttackState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
            attackType = context.attackInfo.attackType;
            attackStrategies = new IStrategy[2]
            {
                new MeleeAttack(context, transitionTo),
                new RangedAttack(context,transitionTo)
            };
        }

        public override void OnStart()
        {
            Debug.Log("Enemy AttackState");
            context.enemyInfo.setSight.SetValue(context.chaseInfo.sightRadius, context.chaseInfo.sightAngle);
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            context.enemyInfo.rigid.gravityScale = context.enemyInfo.gravityStrength;

            attackStrategies[(int)attackType].Initialize();
            isAttacking = true;
        }

        public override void OnUpdate()
        {
            TransitionAni();
            TransitionState();
            attackStrategies[(int)attackType].Execute();
        }

        public override void OnExit()
        {
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), false);
        }
        private void TransitionAni()
        {
            if (!IsAttackAni()) return;
            if (!IsAttackAniEnd()) return;
            isAttacking = false;
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
            interval += Time.deltaTime;
            if (interval < context.enemyInfo.animator.GetCurrentAnimatorStateInfo(0).length)
                return false;
            interval = 0;
            return true;

        }
        protected override void TransitionState()
        {
            if (isAttacking) return;
            if (IsHitState()) return;
            if (IsChaseState()) return;
        }
        private bool IsChaseState()
        {
            if (!context.IsChaseState) return false;
            transitionTo((int)EEnemyState.Chase);

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