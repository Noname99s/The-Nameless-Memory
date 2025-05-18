using System.Collections.Generic;
using UnityEngine;
using Orpul1.State;
using Orpul1.Player.State;
using UnityEditor;
namespace Orpul1.Player
{
    public class PlayerStateFactory : MonoBehaviour, IFactoryable<List<IState>>
    {
        [SerializeField] PlayerContext playerContext;
        public List<IState> Create()
        {
            List<IState> playerStates = new List<IState>()
            {
                new IdleState(playerContext),
                new MoveState(playerContext),
            };
            return playerStates;
        }

    }

}
