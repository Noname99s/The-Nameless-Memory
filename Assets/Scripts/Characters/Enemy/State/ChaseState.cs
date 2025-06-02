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
            Vector2 dir = (context.chaseInfo.target.position - context.transform.position).normalized;
            dir = dir * context.chaseInfo.chaseMoveSpeed;
            context.enemyInfo.slideResult = context.enemyInfo.rigid.Slide(Vector2.right * dir.x, Time.deltaTime, context.enemyInfo.slideMovement);
            context.enemyInfo.animator.SetFloat(EEnemyAni.MoveSpeed.ToString(), 1);
        }

        public override void OnExit()
        {
        }
    }
}