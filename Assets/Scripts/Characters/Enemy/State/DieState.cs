using Orpul1.Enemy;
using Orpul1.Enemy.Sound;
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
            Debug.Log("Enemy DieState");
            context.enemyInfo.animator.SetBool(EEnemyAniTransition.DieTrigger.ToString(), true);

            context.enemyInfo.rigid.gravityScale = 0;
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            context.enemyInfo.enemyCollider.enabled = false;
            dieStateEnterTime = Time.time + context.dieInfo.deactiveTime;
            context.enemyInfo.sound.PlaySound((int)EEnemySound.Death);
        }

        public override void OnUpdate()
        {
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
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