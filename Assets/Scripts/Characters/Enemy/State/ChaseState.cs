using System;
using Orpul1.Player.Animation;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class ChaseState : EnemyState
    {
        public ChaseState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
        }

        public override void OnStart()
        {

            Debug.Log("Enemy ChaseState");
            context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), context.chaseInfo.chaseMoveSpeed);
            context.enemyInfo.setSight.SetValue(context.chaseInfo.sightRadius, context.chaseInfo.sightAngle);
        }

        public override void OnUpdate()
        {
            DoChase();
            TransitionState();
        }

        public override void OnExit()
        {
        }
        private void DoChase()
        {
            if (context.chaseInfo.chaseTarget == null) return;
            Vector2 dir = (context.chaseInfo.chaseTarget.position - context.transform.position).normalized;
            context.enemyInfo.flipX(dir.x);
            if (!IsOnGround()) return;

            context.enemyInfo.slideResult = context.enemyInfo.rigid.Slide(Vector2.right * dir.x,
                                                                            Time.deltaTime * context.chaseInfo.chaseMoveSpeed,
                                                                            context.enemyInfo.slideMovement);
            context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), 1);
        }

        private bool IsOnGround()
        {
            context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), context.enemyInfo.slideResult.remainingVelocity.x);
            return context.enemyInfo.isGround;
        }
        protected override void TransitionState()
        {
            if (IsPatrolState()) return;
            if (IsAttackState()) return;
            if (IsDieState()) return;
        }
        private bool IsPatrolState()
        {
            if (!context.IsPatrolState) return false;
            transitionTo((int)EEnemyState.Patrol);

            return true;
        }
        private bool IsAttackState()
        {
            if (!context.IsAttackState) return false;
            transitionTo((int)EEnemyState.Attack);

            return true;
        }


        private bool IsDieState()
        {
            if (!context.IsDieState) return false;
            transitionTo((int)EEnemyState.Die);

            return true;
        }
    }
}