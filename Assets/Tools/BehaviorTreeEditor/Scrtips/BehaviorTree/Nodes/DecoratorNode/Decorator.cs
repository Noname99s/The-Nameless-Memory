using UnityEngine;

namespace SOBehaviorTree
{
    public abstract class Decorator : ScriptableObject
    {
        public abstract bool Condition();
    }
}
