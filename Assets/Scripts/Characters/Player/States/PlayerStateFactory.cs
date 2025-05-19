using System.Collections.Generic;
using UnityEngine;
using Orpul1.State;
using Orpul1.Player.State;
using UnityEditor;
using System;
namespace Orpul1.Player
{
    public class PlayerStateFactory : MonoBehaviour, IStateFactory<List<IState>>
    {
        [SerializeField] PlayerContext playerData;
        public List<IState> Create(Action<int> transitionTo)
        {
            return new List<IState>()
            {
                new IdleState(playerData, transitionTo),
                new MoveState(playerData, transitionTo),
                new JumpState(playerData, transitionTo),
                new FallState(playerData, transitionTo)
            };
        }

    }

}
