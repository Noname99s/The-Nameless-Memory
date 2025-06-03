using UnityEngine;

namespace Orpul1.Enemy.State.Attack
{
    public class RangedAttack : IStrategy
    {
        EnemyContext context;
        EnemyFactory arrowFactory;
        public RangedAttack(EnemyContext context)
        {
            arrowFactory = context.attackInfo.arrowFactory;
            this.context = context;
        }
        public void Initialize()
        {
            arrowFactory.Create<GameObject>();
        }
        public void Execute()
        {
        }
    }
}