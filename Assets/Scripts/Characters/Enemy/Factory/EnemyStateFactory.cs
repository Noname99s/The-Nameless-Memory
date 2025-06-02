using System;
using System.Collections.Generic;
using Orpul1.State;
using UnityEngine;

namespace Orpul1.Enemy.State
{
    public class EnemyStateFactory : StateFactory
    {
        [SerializeField] EnemyContext context;
        public override List<IState> Create(Action<int> transitionTo)
        {
            return new List<IState>()
            {
                new IdleState(context,transitionTo),
                new PatrolState(context,transitionTo),
                new ChaseState(context,transitionTo),
                null, // attack
                null, // hit
                new DieState(context,transitionTo),
            };
        }
    }
}
