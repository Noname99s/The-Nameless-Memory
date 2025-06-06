using UnityEngine;

namespace Orpul1.Enemy.DB
{
    [CreateAssetMenu(fileName = "EnemyContextDB", menuName = "DataBase/EnemyContextDB")]
    public class EnemyContextDB : ScriptableObject
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
        public void SetDB(EnemyContext context)
        {
            SetEnemyInfo(context.enemyInfo);
            SetEnemyIdleInfo(context.idleInfo);
            SetEnemyPatrolInfo(context.patrolInfo);
            SetEnemyChaseInfo(context.chaseInfo);
            SetEnemyAttackInfo(context.attackInfo);
            SetEnemyHitInfo(context.hitInfo);
            SetEnemyDieInfo(context.dieInfo);
            SetEnemySightInfo(context.sightInfo);
        }
        public void SetEnemyInfo(EnemyInfo enemyInfo)
        {
            int index = (int)EEnemyContextDB.EnemyInfoStart;
            enemyInfo.gravityStrength = (int)BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            enemyInfo.hp = (int)BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
        }
        public void SetEnemyIdleInfo(EnemyIdleInfo idleInfo)
        {
            int index = (int)EEnemyContextDB.EnemyIdleInfoStart;
            idleInfo.idleDuration= BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
        }
        public void SetEnemyPatrolInfo(EnemyPatrolInfo patrolInfo)
        {
            int index = (int)EEnemyContextDB.EnemyPatrolInfoStart;
            patrolInfo.patrolMoveSpeed = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            patrolInfo.sightRadius = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            patrolInfo.patrolMoveSpeed = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
        }
        public void SetEnemyChaseInfo(EnemyChaseInfo chaseInfo)
        {
            int index = (int)EEnemyContextDB.EnemyChaseInfoStart;
            chaseInfo.chaseMoveSpeed = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            chaseInfo.chaseDistance = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            chaseInfo.sightRadius = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
        }
        public void SetEnemyAttackInfo(EnemyAttackInfo attackInfo)
        {
            int index = (int)EEnemyContextDB.EnemyAttackInfoStart;
            attackInfo.attackDelay = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            attackInfo.attackDistance = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            attackInfo.damage = (int)BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
        }
        public void SetEnemyHitInfo(EnemyHitInfo hitInfo)
        {
            int index = (int)EEnemyContextDB.EnemyHitInfoStart;
            hitInfo.hitDuration = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            hitInfo.knockBackPower = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            hitInfo.blinkCount = (int)BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            hitInfo.hitDuration = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
        }
        public void SetEnemyDieInfo(EnemyDieInfo dieInfo)
        {
            int index = (int)EEnemyContextDB.EnemyDieInfoStart;
            dieInfo.deactiveTime = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
        }
        public void SetEnemySightInfo(SightInfo sightInfo)
        {
            int index = (int)EEnemyContextDB.SightInfoStart;
            sightInfo.angle = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            sightInfo.radius = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
            sightInfo.rotateZ = BG_DB_Enemydata.GetEntity((int)enemyType)._EnemyStatus[index++]._InitiValue;
        }
    }
}
