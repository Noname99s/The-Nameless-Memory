using Orpul1.Enemy;
using System;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class DieState : EnemyState
    {
        float dieStateEnterTime = 0;
        public DieState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
        }

        public override void OnStart()
        {
            context.enemyInfo.animator.SetBool(EEnemyAni.DieTrigger.ToString(), true);
            context.enemyInfo.rigid.linearVelocityX = 0;
            dieStateEnterTime = Time.time + context.dieInfo.deactiveTime;
            Debug.Log("DieState Start");
        }

        public override void OnUpdate()
        {
            if (Time.time < dieStateEnterTime) return;
            DeactiveEnemy();
        }

        public override void OnExit()
        {
        }
        private void DeactiveEnemy()
        {
            if (context.dieInfo.deactiveAction != null)
                context.dieInfo.deactiveAction(context.gameObject);
            else
                context.dieInfo.destroyAction();
        }

        protected override void TransitionState()
        {
        }
    }
}