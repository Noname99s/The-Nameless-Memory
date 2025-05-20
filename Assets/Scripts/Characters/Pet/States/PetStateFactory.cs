using UnityEngine;
using Orpul1.State;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Orpul1.Pet.State;

namespace Orpul1.Pet
{
    public class PetStateFactory : StateFactory
    {
        [SerializeField] PetContext context;
        public override List<IState> Create(Action<int> transitionTo)
        {
            return new List<IState>()
            {
                new FollowState(context,transitionTo),
            };
        }
    }
}
