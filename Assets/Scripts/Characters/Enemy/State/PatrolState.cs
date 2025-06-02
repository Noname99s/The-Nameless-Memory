using System;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class PatrolState : EnemyState
    {
        public PatrolState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
        }
        int moveIndex = 0;
        public override void OnStart()
        {
            context.enemyInfo.animator.SetFloat(EEnemyAni.MoveSpeed.ToString(), 1);
            context.enemyInfo.rigid.gravityScale = 0;
        }

        public override void OnUpdate()
        {
            MovePosi();
            if (IsArrivePosi())
            {
                moveIndex = (moveIndex + 1) % context.patrolInfo.movePosis.Length;
                transitionTo((int)EEnemyState.Idle);
            }
        }

        public override void OnExit()
        {
            context.enemyInfo.animator.SetFloat(EEnemyAni.MoveSpeed.ToString(), 0);
        }
        private void MovePosi()
        {
            Vector2 dir = (context.patrolInfo.movePosis[moveIndex].position - context.transform.position).normalized;
            context.enemyInfo.slideResult = context.enemyInfo.rigid.Slide(Vector2.right * dir.x,
                                                                            Time.deltaTime * context.patrolInfo.patrolMoveSpeed,
                                                                            context.enemyInfo.slideMovement);
            context.enemyInfo.flipX(dir.x);
        }
        private bool IsArrivePosi()
        {
            float distance = Mathf.Abs(context.patrolInfo.movePosis[moveIndex].position.x - context.transform.position.x);
            if (distance > 0.1) return false;
            return true;
        }

        protected override void TransitionState()
        {
            if (IsIdleState()) return;
            if (IsChaseState()) return;
        }

        private bool IsIdleState()
        {
            if (!context.IsIdleState) return false;
            transitionTo((int)EEnemyState.Idle);

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
            return true;
        }
        private bool IsDieState()
        {
            return true;
        }
    }
}
