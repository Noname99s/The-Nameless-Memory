using UnityEngine;

namespace SOBehaviorTree
{
    public abstract class Decorator : ScriptableObject
    {
        public virtual void Initialize()
        {

        }
        public abstract bool Condition();
    }
}
