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
            transitionTo((int)EEnemyState.Idle);
        }
    }
}
