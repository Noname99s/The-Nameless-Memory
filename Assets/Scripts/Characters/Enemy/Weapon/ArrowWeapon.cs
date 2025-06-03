using BansheeGz.BGDatabase;
using System.Collections;
using UnityEngine;

namespace Orpul1.Enemy.Weapon
{
    public class ArrowWeapon : EnemyWeapon
    {
        [SerializeField] float deactiveTime;
        [SerializeField] Animator animator;
        Vector2 shootDir = Vector2.zero;
        bool isExecute = false;
        public override void Execute()
        {
            isExecute = true;
        }

        public override void ResetValue()
        {
            isExecute = false;
        }

        public override void SetValue(Vector2 dir, float damage)
        {
            shootDir = dir;
            this.damage = damage;
        }

        private void FixedUpdate()
        {
            if (!isExecute) return;
        }
    }
}