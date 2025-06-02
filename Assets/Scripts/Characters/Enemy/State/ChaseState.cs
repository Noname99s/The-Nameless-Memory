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
            context.enemyInfo.animator.SetFloat(EEnemyAni.MoveSpeed.ToString(), context.chaseInfo.chaseMoveSpeed);
        }

        public override void OnUpdate()
        {
            Vector2 dir = (context.chaseInfo.chaseTarget.position - context.transform.position).normalized;
            context.enemyInfo.slideResult = context.enemyInfo.rigid.Slide(Vector2.right * dir.x,
                                                                            Time.deltaTime * context.chaseInfo.chaseMoveSpeed,
                                                                            context.enemyInfo.slideMovement);
            context.enemyInfo.animator.SetFloat(EEnemyAni.MoveSpeed.ToString(), 1);

            context.enemyInfo.flipX(dir.x);
        }

        public override void OnExit()
        {
        }

        protected override void TransitionState()
        {
            if (IsPatrolState()) return;
        }
        private bool IsPatrolState()
        {
            if (!context.IsPatrolState) return false;
            transitionTo((int)EEnemyState.Patrol);

            return true;
        }
    }
}