using System;
using UnityEngine;

namespace Orpul1.Enemy.TestEnemy
{
    public class MeleeEnemy : EnemyFactory
    {
        public override T Create<T>()
        {
            T enemy = GetObject<T>();
            ResetEnemy(enemy as IResetAble);
            SetEnqueueAction(enemy as ISetDataAble<Action<GameObject>>);
            return enemy;
        }

        private void ResetEnemy(IResetAble enemy)
        {
            enemy.ResetValue();
        }
        private void SetEnqueueAction(ISetDataAble<Action<GameObject>> enemy)
        {
            enemy.SetValue(EnqueueObject);
        }
    }
}
