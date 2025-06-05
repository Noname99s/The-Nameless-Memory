using System;
using Orpul1.Enemy.DB;
using UnityEngine;

namespace Orpul1.Enemy
{
    [Serializable]
    public class EnemyInfo
    {
        public Rigidbody2D rigid;
        public Rigidbody2D.SlideMovement slideMovement = new Rigidbody2D.SlideMovement();
        public Rigidbody2D.SlideResults slideResult = new Rigidbody2D.SlideResults();
        public SpriteRenderer sprite;
        public Animator animator;
        public Collider2D enemyCollider;
        public Vector3 originScale = Vector3.one;
        public Action<float> flipX;
        public ISetDataAble<float, float> setSight;
        public float gravityStrength;
        public bool isGround = true;
        public bool isAlwayChasePlayer = false;
        public int hp;
        public int flipValue = 1;
    }
    [Serializable]
    public class EnemyPatrolInfo
    {
        public enum EPatrolType
        {
            Path,
            Free,
        }
        public EPatrolType patrolType;
        [Range(0,360)] public float sightAngle;
        public float patrolMoveSpeed;

        [Header("Path Patrol")]
        public Transform[] movePosis;
        public float sightRadius;

        [Header("Free Patrol")]
        public float moveTime;

    }

    [Serializable]
    public class  EnemyIdleInfo
    {
        public float idleDuration;
    }

    [Serializable]
    public class EnemyChaseInfo
    {
        public Transform chaseTarget;
        public bool isFindChaseTarget = false;
        public float chaseMoveSpeed;
        public float chaseDistance;
        [Range(0, 360)] public float sightAngle;
        public float sightRadius;
    }

    [Serializable]
    public class EnemyAttackInfo
    {
        public EEnemyAttack attackType = EEnemyAttack.MeleeAttack;
        public EnemyFactory arrowFactory;
        public float attackDelay;
        public float attackDistance;
        public float arrowSpeed;
        public int damage;
    }
    [Serializable]
    public class EnemyHitInfo
    {
        public enum EBlinkType
        {
            OnBlink,
            OffBlink,
        }
        public float hitDuration;
        public float knockBackPower;
        public int blinkCount = 3;
        public float blinkDuration = 0.1f;
        public Color[] blinkColors;
        public WaitForSeconds waitNextBlink;
        public bool stillBlink = false;
        public bool isHit = false;
    }
    [Serializable] 
    public class EnemyDieInfo
    {
        public Action<GameObject> deactiveAction;
        public Action destroyAction;
        public bool isDie = false;
        public float deactiveTime = 2;
    }
    public class EnemyContext : MonoBehaviour
    {
        [SerializeField] FieldOfView fieldOfView;
        [SerializeField] EnemyContextDB enemyContextDB;
        public EnemyInfo enemyInfo;
        public EnemyIdleInfo idleInfo;
        public EnemyPatrolInfo patrolInfo;
        public EnemyChaseInfo chaseInfo;
        public EnemyAttackInfo attackInfo;
        public EnemyHitInfo hitInfo;
        public EnemyDieInfo dieInfo;
        public SightInfo sightInfo;
        public void Initialize()
        {
            /*SetInfos();
            SetSightInfo();*/
            hitInfo.waitNextBlink = new WaitForSeconds(hitInfo.blinkDuration);
        }
        private void SetInfos()
        {
            IVisitAble dbVisit = enemyContextDB as IVisitAble;
            if (dbVisit == null) return;

            dbVisit.Visit<EnemyInfo>(enemyInfo);
            dbVisit.Visit<EnemyIdleInfo>(idleInfo);
            dbVisit.Visit<EnemyPatrolInfo>(patrolInfo);
            dbVisit.Visit<EnemyChaseInfo>(chaseInfo);
            dbVisit.Visit<EnemyAttackInfo>(attackInfo);
            dbVisit.Visit<EnemyHitInfo>(hitInfo);
            dbVisit.Visit<EnemyDieInfo>(dieInfo);
            dbVisit.Visit<SightInfo>(sightInfo);
        }
        private void SetSightInfo()
        {
            IVisitAble visit = fieldOfView as IVisitAble;
            if (visit == null)
            {
                Debug.LogError("IVisitAble is Null of sightView ");
                return;
            }
            visit.Visit(sightInfo);
        }
        public bool IsPatrolState
        {
            get { return !chaseInfo.isFindChaseTarget; }
        }
        public bool IsChaseState
        {
            get 
            {
                if (!chaseInfo.isFindChaseTarget)
                    return false;
                float distance = Vector2.Distance(transform.position, chaseInfo.chaseTarget.position);

                return distance < chaseInfo.chaseDistance && distance > attackInfo.attackDistance;
            }
        }

        public bool IsAttackState
        {
            get
            {
                if (!chaseInfo.isFindChaseTarget)
                    return false;

                float distance = Vector2.Distance(transform.position, chaseInfo.chaseTarget.position);

                return distance <= attackInfo.attackDistance; 
            }
        }
        public bool IsHitState
        {
            get
            {
                return hitInfo.isHit;
            }
        }
        public bool IsDieState
        {
            get { return enemyInfo.hp <= 0; }
        }
    }

}
