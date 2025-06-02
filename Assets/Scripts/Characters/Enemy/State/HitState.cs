using System;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class HitState : EnemyState
    {
        float hitStateEnterTime = 0;
        public HitState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
        }
        public override void OnStart()
        {
            context.enemyInfo.animator.SetTrigger(EEnemyAniTransition.HitTrigger.ToString());
            hitStateEnterTime = Time.time + context.hitInfo.hitDuration;
            context.enemyInfo.rigid.gravityScale = context.enemyInfo.gravityStrength;
        }

        public override void OnUpdate()
        {
            IsDieState();
            if (Time.time < hitStateEnterTime) return;
            TransitionState();
        }

        public override void OnExit()
        {
            context.hitInfo.isHit = false;
            context.enemyInfo.rigid.gravityScale = 0;
        }


        protected override void TransitionState()
        {
            if (IsPatrolState()) return;
            if (IsChaseState()) return;
            if (IsAttackState()) return;
        }
        private bool IsPatrolState()
        {
            if (!context.IsPatrolState) return false;
            transitionTo((int)EEnemyState.Patrol);

            return true;
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
        private bool IsDieState()
        {
            if (!context.IsDieState) return false;
            transitionTo((int)EEnemyState.Die);

            return true;
        }
        
    }
}
