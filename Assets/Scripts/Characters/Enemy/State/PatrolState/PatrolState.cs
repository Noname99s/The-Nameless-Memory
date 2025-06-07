using System;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class PatrolState : EnemyState
    {
        IStrategy[] patrolStraties;
        public PatrolState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
            patrolStraties = new IStrategy[]
            {
                new PatrolPath(context,transitionTo),
                new PatrolFree(context,transitionTo),
            };
        }
        public override void OnStart()
        {

            Debug.Log("Enemy PatrolState");
            context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), 1);
            context.enemyInfo.rigid.gravityScale = 0;
            context.enemyInfo.setSight.SetValue(context.patrolInfo.sightRadius, context.patrolInfo.sightAngle);
            patrolStraties[(int)context.patrolInfo.patrolType].Initialize();
        }

        public override void OnUpdate()
        {
            patrolStraties[(int)context.patrolInfo.patrolType].Execute();
            TransitionState();
        }

        public override void OnExit()
        {
            context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), 0);
        }

        protected override void TransitionState()
        {
            if (IsChaseState()) return;
            if (IsAttackState()) return;
            if (IsDieState()) return;
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
