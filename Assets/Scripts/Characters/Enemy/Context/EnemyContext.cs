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
    }
    [Serializable]
    public class PatrolInfo
    {
        public Transform[] movePosis;
        public float patrolMoveSpeed;
    }
    [Serializable]
    public class ChaseInfo
    {
        public Transform target;
        public float chaseMoveSpeed;
    }
    [Serializable]
    public class AttackInfo
    {
        public int damage;
    }
    public class EnemyContext : MonoBehaviour
    {
        public EnemyInfo enemyInfo;
        public PatrolInfo patrolInfo;
        public ChaseInfo chaseInfo;
        public AttackInfo attackInfo;
    }

}
