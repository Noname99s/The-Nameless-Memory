using System;
using UnityEngine;

namespace Orpul1.Enemy.Weapon
{
    public class EnemyArrowFactory : EnemyFactory
    {
        [SerializeField] Transform shootPosition;
        protected override void Initialize()
        {
            if (parent == null) 
                parent = new GameObject("ArrowPool");
            base.Initialize();
        }
        public override T Create<T>()
        {
            T arrow = GetObject<T>();
            ResetArrow(arrow as IResetAble);
            SetPosition(arrow as ArrowWeapon);
            SetEnqueueAction(arrow as ISetDataAble<Action<GameObject>>);
            return arrow;
        }
        private void SetPosition(ArrowWeapon arrow)
        {
            if (arrow == null) return;
            arrow.transform.position = shootPosition.position;
        }
        private void ResetArrow(IResetAble arrow)
        {
            if (arrow == null) return;
            arrow.ResetValue();
        }
        private void SetEnqueueAction(ISetDataAble<Action<GameObject>> arrow)
        {
            if (arrow == null) return;
            arrow.SetValue(EnqueueObject);
        }

        public override void Deactive()
        {
        }
    }
}