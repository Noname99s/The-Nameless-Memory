using System;
using UnityEngine;
namespace Orpul1.Enemy.State
{
    public class IdleState : EnemyState
    {
        float idleEnterTime = 0;
        public IdleState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
        }

        public override void OnStart()
        {
            context.enemyInfo.rigid.linearVelocityX = 0;
            idleEnterTime = Time.time + context.idleInfo.idleDuration;
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
            transitionTo((int)EEnemyState.Patrol);
        }
        protected override void TransitionState()
        {
            if (IsChaseState()) return;
            if (IsDieState()) return;
        }
        private bool IsChaseState()
        {
            if (!context.IsChaseState) return false;
            transitionTo((int)EEnemyState.Chase);

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
