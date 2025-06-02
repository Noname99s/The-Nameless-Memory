using Orpul1.Enemy.State.Attack;
using System;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class AttackState : EnemyState
    {
        IStrategy[] attackStrategies ;
        public AttackState(EnemyContext context, Action<int> transitionTo) : base(context, transitionTo)
        {
            attackStrategies = new IStrategy[2]
            {
                new MeleeAttack(),
                new RangedAttack()
            };
        }

        public override void OnStart()
        {
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }

        protected override void TransitionState()
        {
        }
    }
}