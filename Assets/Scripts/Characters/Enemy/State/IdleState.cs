using System;
using UnityEngine;
namespace Orpul1.Enemy.State
{
    public class IdleState : EnemyState
    {
        public IdleState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
        }

        public override void OnStart()
        {
            context.enemyInfo.rigid.linearVelocityX = 0;
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }
    }
}
