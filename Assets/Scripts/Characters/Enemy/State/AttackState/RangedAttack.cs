using Orpul1.Enemy.Weapon;
using System;
using UnityEngine;

namespace Orpul1.Enemy.State.Attack
{
    public class RangedAttack : IStrategy
    {
        EnemyContext context;
        EnemyFactory arrowFactory;
        Action<int> transitionTo;
        float attackStateEnterTime = 0;
        float attackTime = 0;
        bool isAttack = false;
        bool isAttackEnd;
        public RangedAttack(EnemyContext context, Action<int> transitionTo)
        {
            arrowFactory = context.attackInfo.arrowFactory;
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
            if (attackTime <= Time.time && !isAttack) 
            {
                ArrowWeapon arrow = arrowFactory.Create<ArrowWeapon>();
                SetArrow(arrow);
                arrow.Execute();
                isAttack = true;
            }
            if (attackStateEnterTime > Time.time) return;
            transitionTo((int)EEnemyState.Idle);
        }
        private void SetArrow(ISetDataAble<Vector3, int> arrow)
        {
            if (arrow == null) return;
            //1. dir , 2. damage
            Vector3 dir = context.chaseInfo.chaseTarget.position - context.transform.position;
            arrow.SetValue(dir.normalized, context.attackInfo.damage);
        }
        private void AbleAttack()
        {
            if (attackStateEnterTime < Time.time)
            {
                attackStateEnterTime = 0;
                isAttackEnd = true;
                isAttack = false;
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
            attackTime = Time.time + 0.4f;

           /* ArrowWeapon arrow = arrowFactory.Create<ArrowWeapon>();
            SetArrow(arrow);
            arrow.Execute();*/
        }
        private void FlipX()
        {
            Vector2 dir = context.chaseInfo.chaseTarget.position - context.transform.position;
            context.enemyInfo.flipX(dir.normalized.x);
        }
    }
}