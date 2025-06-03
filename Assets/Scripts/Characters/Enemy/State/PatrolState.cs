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

            Debug.Log("Enemy PatrolState");
            context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), 1);
            context.enemyInfo.rigid.gravityScale = 0;
            context.enemyInfo.setSight.SetValue(context.patrolInfo.sightRadius, context.patrolInfo.sightAngle);
        }

        public override void OnUpdate()
        {
            MovePosi();
            if (IsArrivePosi())
            {
                moveIndex = (moveIndex + 1) % context.patrolInfo.movePosis.Length;
                transitionTo((int)EEnemyState.Idle);
            }
            TransitionState();
        }

        public override void OnExit()
        {
            context.enemyInfo.animator.SetFloat(EEnemyAniTransition.MoveSpeed.ToString(), 0);
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
