using System;
using UnityEditor.EditorTools;
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
        public Vector3 originScale = Vector3.one;
        public Action<float> flipX;
        public ISetDataAble<float, float> setSight;
        public float gravityStrength;
        public int hp;
        public int flipValue = 1;
    }
    [Serializable]
    public class EnemyPatrolInfo
    {
        public Transform[] movePosis;
        public float patrolMoveSpeed;
        [Range(0,360)] public float sightAngle;
        public float sightRadius;
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
        public float attackDistance;
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
        public EnemyInfo enemyInfo;
        public EnemyIdleInfo idleInfo;
        public EnemyPatrolInfo patrolInfo;
        public EnemyChaseInfo chaseInfo;
        public EnemyAttackInfo attackInfo;
        public EnemyHitInfo hitInfo;
        public EnemyDieInfo dieInfo;
        public void Initialize()
        {
            hitInfo.waitNextBlink = new WaitForSeconds(hitInfo.blinkDuration);
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
