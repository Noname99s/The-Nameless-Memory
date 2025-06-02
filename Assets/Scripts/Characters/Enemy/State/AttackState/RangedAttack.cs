using UnityEngine;

namespace Orpul1.Enemy.State.Attack
{
    public class RangedAttack : IStrategy
    {
        EnemyContext context;
        public RangedAttack(EnemyContext context)
        {
            this.context = context;
        }
        public void Initialize()
        {
        }
        public void Execute()
        {
        }
    }
}