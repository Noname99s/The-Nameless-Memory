using System;
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
    }
    [Serializable] 
    public class EnemyDieInfo
    {
        public Action<GameObject> deactiveAction;
    }
    public class EnemyContext : MonoBehaviour
    {
        public EnemyInfo enemyInfo;
        public EnemyIdleInfo idleInfo;
        public EnemyPatrolInfo patrolInfo;
        public EnemyChaseInfo chaseInfo;
        public EnemyAttackInfo attackInfo;
        public EnemyDieInfo dieInfo;

        public bool IsIdleState
        {
            get { return true; }
        }
        public bool IsPatrolState
        {
            get { return !chaseInfo.isFindChaseTarget; }
        }
        public bool IsChaseState
        {
            get { return chaseInfo.isFindChaseTarget; }
        }
    }

}
