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
        [SerializeField] PlayerContext playerContext;
        public List<IState> Create(Action<int> transitionTo)
        {
            List<IState> playerStates = new List<IState>()
            {
                new IdleState(playerContext, transitionTo),
                new MoveState(playerContext, transitionTo),
            };
            return playerStates;
        }

    }

}
