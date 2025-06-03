using UnityEngine;

namespace Orpul1.Enemy.Weapon
{
    public abstract class EnemyWeapon : MonoBehaviour, IResetAble, ISetDataAble<Vector2, float>
    {
        [SerializeField] protected float damage;
        public abstract void ResetValue();
        public abstract void SetValue(Vector2 t1, float t2);
        public abstract void Execute();
    }
}