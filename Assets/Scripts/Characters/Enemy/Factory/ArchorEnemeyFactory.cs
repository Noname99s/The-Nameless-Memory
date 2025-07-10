using System;
using UnityEngine;
using UnityEngine.Events;
namespace Orpul1.Enemy.TestEnemy
{
    public class ArchorEnemeyFactory : EnemyFactory
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
            SetDieEvent(enemy as IDieAble);
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
        private void SetDieEvent(IDieAble enemy)
        {
            deactiveEvents.AddListener(enemy.Die);
        }
        public override void Deactive()
        {
            deactiveEvents.Invoke();
        }
    }
}
