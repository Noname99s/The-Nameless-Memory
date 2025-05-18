
using Orpul1.Player;
using System;

namespace Orpul1
{
    public interface IStateFactory<T>
    {
        public T Create(Action<int> transitionTo);
    }

}
