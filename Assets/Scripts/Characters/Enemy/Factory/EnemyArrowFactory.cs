using UnityEngine;

namespace Orpul1.Enemy.Weapon
{
    public class EnemyArrowFactory : EnemyFactory
    {
        public override T Create<T>()
        {
            if (parent == null)
                parent = new GameObject("ArrowPool");
            return GetObject<T>();
        }
    }
}