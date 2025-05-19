using UnityEngine;
using System;
namespace Orpul1.Player.State
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) {}

        public override void OnStart()
        {
        }
        public override void OnUpdate()
        {

        }
        public override void OnExit()
        {
        }
    }
}
