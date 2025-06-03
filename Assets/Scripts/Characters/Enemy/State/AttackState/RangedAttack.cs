using Orpul1.Enemy.Weapon;
using System;
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
            
        }
        public void Execute()
        {
            ArrowWeapon arrow = arrowFactory.Create<ArrowWeapon>();
            SetArrow(arrow);
            arrow.Execute();
        }
        private void SetArrow(ISetDataAble<Vector3, float> arrow)
        {
            if (arrow == null) return;
            //1. targetPosi , 2. damage
            arrow.SetValue(context.chaseInfo.chaseTarget.position, context.attackInfo.damage);
        }
    }
}