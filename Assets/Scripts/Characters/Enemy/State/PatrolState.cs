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
            moveIndex = 0;
        }

        public override void OnUpdate()
        {
            if(IsArrivePosi())
                moveIndex = (moveIndex + 1) % context.patrolInfo.movePosis.Length;

            Vector2 dir = (context.patrolInfo.movePosis[moveIndex].position - context.transform.position).normalized;
            context.enemyInfo.slideResult = context.enemyInfo.rigid.Slide(Vector2.right * dir.x, 
                                                                            Time.deltaTime * context.patrolInfo.patrolMoveSpeed,
                                                                            context.enemyInfo.slideMovement);
        }

        public override void OnExit()
        {
        }
        private bool IsArrivePosi()
        {
            float distance = Mathf.Abs(context.patrolInfo.movePosis[moveIndex].position.x - context.transform.position.x);
            if (distance > 0.1) return false;
            return true;
        }
    }
}
