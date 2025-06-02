using System;
using Orpul1;
using Orpul1.Enemy.State;
using Orpul1.State;
using UnityEngine;
using UnityEngine.Experimental.AI;
namespace Orpul1.Enemy
{
    [Serializable]
    public class SightInfo
    {
        public Transform trTarget;
        public Vector3 originPosi;
        public LayerMask detectMask;
        public float viewRadius = 1;
        public float distance = 0;
        public int flipValue = 1;

        public Ray ray;
        public RaycastHit2D hit;
    }
    public class EnemyController : MonoBehaviour, ISetDataAble<Action<GameObject>>, IResetAble
    {
        [SerializeField] SightInfo sightInfo;
        [SerializeField] StateController stateController;
        [SerializeField] EnemyContext context;
        [SerializeField] EEnemyState state;
        ISightFindAble enemySight;

        private void Awake()
        {
            enemySight = new FieldOfView(sightInfo);
        }
        private void Start()
        {
            context.enemyInfo.flipX = FlipX;
            stateController.TransitionState((int)state);
        }
        void Update()
        {
            LookForward();
        }
        private void LookForward()
        {
            SearchTarget();
            //TODO: 낭떠러지인지 확인
        }
        private void SearchTarget()
        {
            Transform target = enemySight.DetectTarget();
            if (!IsFindTarget(target))
            {
                context.chaseInfo.isFindChaseTarget = false;
                return;
            }

            context.chaseInfo.isFindChaseTarget = true;
            context.chaseInfo.chaseTarget = target;
        }
        private bool IsFindTarget(Transform target)
        {
            return !(target == null);
        }

        private void FixedUpdate()
        {
            stateController.ProcessState();
        }
        private void FlipX(float dir)
        {
            float xScale = Mathf.RoundToInt(dir);
            if (xScale == 0) return;

            Vector3 scale = context.enemyInfo.originScale;
            sightInfo.flipValue = scale.x < 0 ? -1 : 1;
            scale.x *= xScale;
            transform.localScale = scale;
        }

        public void ResetValue()
        {
        }

        public void SetValue(Action<GameObject> enqueueAction)
        {
            context.dieInfo.deactiveAction = enqueueAction;
        }


        private void OnDrawGizmos()
        {
            if (IsNullInfo()) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sightInfo.trTarget.position + sightInfo.originPosi, sightInfo.viewRadius);
        }
        private bool IsNullInfo()
        {
            if (sightInfo.trTarget == null) return true;
            return false;
        }
    }
}
