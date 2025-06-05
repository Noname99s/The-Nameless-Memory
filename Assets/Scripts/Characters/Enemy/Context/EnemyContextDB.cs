using System;
using UnityEngine;
using UnityEngine.Events;

namespace Orpul1.Enemy.DB
{
    [CreateAssetMenu(fileName = "EnemyContextDB", menuName = "DataBase/EnemyContextDB")]
    public class EnemyContextDB : ScriptableObject, IVisitAble
    {
        [SerializeField] private EEnemyType enemyType;
        public enum EEnemyContextDB
        {
            EnemyInfoStart = 0,
            EnemyIdleInfoStart = 2,
            EnemyPatrolInfoStart = 3,
            EnemyChaseInfoStart = 6,
            EnemyAttackInfoStart = 9,
            EnemyHitInfoStart = 12,
            EnemyDieInfoStart = 16,
            SightInfoStart = 17,
        }
        public void Visit<T>(T t)
        {
            SetEnemyInfo(t);
            SetEnemyIdleInfo(t);
            SetEnemyPatrolInfo(t);
            SetEnemyChaseInfo(t);
            SetEnemyAttackInfo(t);
            SetEnemyHitInfo(t);
            SetEnemyDieInfo(t);
            SetEnemySightInfo(t);
        }
        public void SetEnemyInfo<T>(T t)
        {
            if (!(t is EnemyInfo info)) return;
            int index = (int)EEnemyContextDB.EnemyInfoStart;
        }
        public void SetEnemyIdleInfo<T>(T t)
        {
            if (!(t is EnemyIdleInfo info)) return;
            int index = (int)EEnemyContextDB.EnemyIdleInfoStart;
        }
        public void SetEnemyPatrolInfo<T>(T t)
        {
            if (!(t is EnemyPatrolInfo info)) return;
            int index = (int)EEnemyContextDB.EnemyPatrolInfoStart;
        }
        public void SetEnemyChaseInfo<T>(T t)
        {
            if (!(t is EnemyChaseInfo info)) return;
            int index = (int)EEnemyContextDB.EnemyChaseInfoStart;
        }
        public void SetEnemyAttackInfo<T>(T t)
        {
            if (!(t is EnemyAttackInfo info)) return;
            int index = (int)EEnemyContextDB.EnemyAttackInfoStart;
        }
        public void SetEnemyHitInfo<T>(T t)
        {
            if (!(t is EnemyHitInfo info)) return;
            int index = (int)EEnemyContextDB.EnemyHitInfoStart;
        }
        public void SetEnemyDieInfo<T>(T t)
        {
            if (!(t is EnemyDieInfo info)) return;
            int index = (int)EEnemyContextDB.EnemyDieInfoStart;
        }
        public void SetEnemySightInfo<T>(T t)
        {
            if (!(t is SightInfo info)) return;
            int index = (int)EEnemyContextDB.SightInfoStart;
        }
    }
}
