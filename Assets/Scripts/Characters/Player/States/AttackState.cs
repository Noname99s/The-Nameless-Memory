using System;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class AttackState : PlayerBaseState
    {
        public AttackState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) { }

        public override void OnStart()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

    }
}
