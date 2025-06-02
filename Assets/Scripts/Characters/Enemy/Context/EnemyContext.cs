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
        public Animator animator;
        public Vector3 originScale = Vector3.one;
        public Action<float> flipX;
        public int hp;
        public int flipValue = 1;
    }
    [Serializable]
    public class EnemyPatrolInfo
    {
        public Transform[] movePosis;
        public float patrolMoveSpeed;
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
    }
    [Serializable]
    public class HitInfo
    {
        public bool isHit = false;
    }
    [Serializable]
    public class EnemyAttackInfo
    {
        public int damage;
        public float attackDistance;
    }
    [Serializable] 
    public class EnemyDieInfo
    {
        public Action<GameObject> deactiveAction;
        public Action destroyAction;
        public float deactiveTime = 2;
    }
    public class EnemyContext : MonoBehaviour
    {
        public EnemyInfo enemyInfo;
        public EnemyIdleInfo idleInfo;
        public EnemyPatrolInfo patrolInfo;
        public EnemyChaseInfo chaseInfo;
        public EnemyAttackInfo attackInfo;
        public EnemyDieInfo dieInfo;

        public bool IsPatrolState
        {
            get { return !chaseInfo.isFindChaseTarget; }
        }
        public bool IsChaseState
        {
            get { return chaseInfo.isFindChaseTarget; }
        }

        public bool IsAttackState
        {
            get
            {
                if (chaseInfo.chaseTarget == null)
                    return false;

                if (Vector2.Distance(chaseInfo.chaseTarget.position, transform.position) > attackInfo.attackDistance)
                    return false;

                return true; 
            }
        }
        public bool IsDieState
        {
            get { return enemyInfo.hp <= 0; }
        }
    }

}
