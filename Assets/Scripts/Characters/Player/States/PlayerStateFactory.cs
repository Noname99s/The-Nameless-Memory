using System.Collections.Generic;
using UnityEngine;
using Orpul1.State;
using Orpul1.Player.State;
using System;
using Orpul1.Player.State.Hit;
namespace Orpul1.Player
{
    public class PlayerStateFactory : StateFactory
    {
        [SerializeField] PlayerContext playerData;
        public override List<IState> Create(Action<int> transitionTo)
        {
            return new List<IState>()
            {
                new IdleState(playerData, transitionTo),
                new MoveState(playerData, transitionTo),
                new JumpState(playerData, transitionTo),
                new FallState(playerData, transitionTo),
                new AttackState(playerData, transitionTo),
                new WallClimb(playerData, transitionTo),
                new HitState(playerData, transitionTo),
                new DeathState(playerData, transitionTo),
                new ClimbLadderState(playerData, transitionTo),
            };
        }

    }

}
