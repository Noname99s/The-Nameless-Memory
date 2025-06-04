using UnityEngine;

namespace Orpul1.Enemy.Weapon
{
    public abstract class EnemyWeapon : MonoBehaviour, IResetAble
    {
        [SerializeField] protected int damage;
        public abstract void ResetValue();
        public abstract void Execute();
    }
}