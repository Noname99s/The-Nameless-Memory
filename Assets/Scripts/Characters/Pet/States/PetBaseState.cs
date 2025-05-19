using System;
using Orpul1.State;

namespace Orpul1.Pet.State
{
    public abstract class PetBaseState : IState
    {
        protected PetContext context;
        Action<int> transitionTo;
        public PetBaseState(PetContext context, Action<int> transitionTo)
        {
            this.context = context;
            this.transitionTo = transitionTo;
        }

        public abstract void OnExit();

        public abstract void OnStart();

        public abstract void OnUpdate();
    }
}