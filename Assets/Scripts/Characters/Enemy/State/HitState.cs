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
            context.enemyInfo.animator.SetTrigger(EEnemyAni.HitTrigger.ToString());
            hitStateEnterTime = Time.time + context.hitInfo.hitDuration;

        }

        public override void OnUpdate()
        {
            if (Time.time < hitStateEnterTime) return;
            TransitionState();
        }

        public override void OnExit()
        {
            context.hitInfo.isHit = false;
        }


        protected override void TransitionState()
        {
            transitionTo((int)EEnemyState.Idle);
        }
    }
}
