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

            attackStrategies[(int)context.attackInfo.attackType].Initialize();
            /*context.enemyInfo.setSight.SetValue(context.chaseInfo.sightRadius, context.chaseInfo.sightAngle);
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            context.enemyInfo.rigid.gravityScale = context.enemyInfo.gravityStrength;
            AbleAttack();
            DoAttack();*/
        }

        public override void OnUpdate()
        {
            TransitionState();
            TransitionAni();

            attackStrategies[(int)context.attackInfo.attackType].Execute();
        }

        public override void OnExit()
        {
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), false);
        }
        private void TransitionAni()
        {
            if (!IsAttackAni()) return;
            if (!IsAttackAniEnd()) return;
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), false);
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
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.IsAttack.ToString(), true);
            attackStrategies[(int)attackType].Execute();
            attackStateEnterTime = Time.time + context.attackInfo.attackDelay;
            isAttackEnd = false;
            interval = 0;
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
            interval += Time.deltaTime;
            if (interval < context.enemyInfo.animator.GetCurrentAnimatorStateInfo(0).length)
                return false;
            interval = 0;
            return true;

        }
        protected override void TransitionState()
        {
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