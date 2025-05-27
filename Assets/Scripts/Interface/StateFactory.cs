
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Orpul1.State
{
    public abstract class StateFactory : MonoBehaviour
    {
        public abstract List<IState> Create(Action<int> transitionTo);
    }

}
