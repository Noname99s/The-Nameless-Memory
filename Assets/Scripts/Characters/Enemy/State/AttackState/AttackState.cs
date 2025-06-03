using Orpul1.Enemy.State.Attack;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class AttackState : EnemyState
    {
        IStrategy[] attackStrategies ;
        EEnemyAttack attackType;
        float interval = 0;
        float attackStateEnterTime = 0;
        bool isAttackEnd = true;
        public AttackState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
            attackType = context.attackInfo.attackType;
            attackStrategies = new IStrategy[2]
            {
                new MeleeAttack(context),
                new RangedAttack(context)
            };
        }

        public override void OnStart()
        {
            Debug.Log("Enemy AttackState");
            context.enemyInfo.setSight.SetValue(context.chaseInfo.sightRadius, context.chaseInfo.sightAngle);
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            context.enemyInfo.rigid.gravityScale = context.enemyInfo.gravityStrength;

            if(isAttackEnd)
            {
                context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), true);
                attackStrategies[(int)attackType].Execute();
                attackStateEnterTime = Time.time + context.attackInfo.attackDelay;
                isAttackEnd = false;
                interval = 0;
            }
            else
            {
                context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), false);
                context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), 0);
            }
        }

        public override void OnUpdate()
        {
            TransitionState();
            FlipX();
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            if (attackStateEnterTime > Time.time) return;
            isAttackEnd = true;
            attackStateEnterTime = 0;
        }

        public override void OnExit()
        {
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), false);
        }
        private void FlipX()
        {
            Vector2 dir = context.chaseInfo.chaseTarget.position - context.transform.position;
            context.enemyInfo.flipX(dir.normalized.x);
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
            if (IsHitState()) return;
            if (IsChaseState()) return;
            if (!isAttackEnd) return;
            transitionTo((int)EEnemyState.Idle);
        }
        private bool IsChaseState()
        {
            if (!context.IsChaseState) return false;
            transitionTo((int)EEnemyState.Chase);

            return true;
        }
        private bool IsAttackState()
        {
            if (!context.IsAttackState) return false;
            transitionTo((int)EEnemyState.Attack);

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