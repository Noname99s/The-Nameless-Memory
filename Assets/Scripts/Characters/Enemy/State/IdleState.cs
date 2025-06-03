using System;
using UnityEngine;
namespace Orpul1.Enemy.State
{
    public class IdleState : EnemyState
    {
        float idleEnterTime = 0;
        bool isIdleEnd = false;
        public IdleState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
        }

        public override void OnStart()
        {

            Debug.Log("Enemy IdleState");
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            idleEnterTime = Time.time + context.idleInfo.idleDuration;
            isIdleEnd = false;
        }

        public override void OnUpdate()
        {
            TransitionState();
            DoIdleBehavior();
        }

        public override void OnExit()
        {
            idleEnterTime = 0;
        }

        private void DoIdleBehavior()
        {
            if (Time.time < idleEnterTime) return;
            isIdleEnd = true;
        }
        protected override void TransitionState()
        {
            if (IsAttackState()) return;
            if (IsChaseState()) return;
            if (IsHitState()) return;
            if (IsDieState()) return;

            if (!isIdleEnd) return;
            if (IsPatrolState()) return;
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
        private bool IsDieState()
        {
            if (!context.IsDieState) return false;
            transitionTo((int)EEnemyState.Die);

            return true;
        }

    }
}
