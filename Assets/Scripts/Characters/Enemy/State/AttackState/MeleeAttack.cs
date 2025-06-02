using UnityEngine;

namespace Orpul1.Enemy.State.Attack
{
    public class MeleeAttack : IStrategy
    {
        EnemyContext context;
        public MeleeAttack(EnemyContext context)
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