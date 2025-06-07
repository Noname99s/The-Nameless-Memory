using System;
using UnityEngine;

namespace Orpul1.Enemy.TestEnemy
{
    public class MeleeEnemy : EnemyFactory
    {
        protected override void Initialize()
        {
            parent = new GameObject(prefab.name);
            base.Initialize();
        }
        public override T Create<T>()
        {
            T enemy = GetObject<T>();
            ResetEnemy(enemy as IResetAble);
            SetEnqueueAction(enemy as ISetDataAble<Action<GameObject>, int>);
            return enemy;
        }

        private void ResetEnemy(IResetAble enemy)
        {
            enemy.ResetValue();
        }
        private void SetEnqueueAction(ISetDataAble<Action<GameObject>,int> enemy)
        {
            enemy.SetValue(EnqueueObject, (int)EnemyPatrolInfo.EPatrolType.Free);
        }
    }
}
