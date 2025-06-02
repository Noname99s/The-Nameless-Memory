using System;
using System.Collections;
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
    public class EnemyController : MonoBehaviour,IDamageAble,IknockbackAble, ISetDataAble<Action<GameObject>>, IResetAble
    {
        [SerializeField] SightInfo sightInfo;
        [SerializeField] StateController stateController;
        [SerializeField] EnemyContext context;
        [SerializeField] EEnemyState state;
        ISightFindAble enemySight;

        private void Awake()
        {
            enemySight = new FieldOfView(sightInfo);
            context.Initialize();
            context.enemyInfo.flipX = FlipX;
            context.dieInfo.destroyAction = DestroyObject;
        }
        private void Start()
        {
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
        private void DestroyObject()
        {
            Destroy(gameObject);
        }
        public void ResetValue()
        {
            stateController.TransitionState((int)state);
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

        public void OnDamage(int damage)
        {
            context.hitInfo.isHit = true;
            context.enemyInfo.hp -= damage;

            if (context.hitInfo.stillBlink) return;
            StartCoroutine("HitBlink");
        }

        public void knockback(Vector2 dir)
        {
            context.enemyInfo.rigid.AddForce(dir * context.hitInfo.knockBackPower, ForceMode2D.Impulse);
        }
        IEnumerator HitBlink()
        {
            context.hitInfo.stillBlink = true;

            for (int i = 0; i < context.hitInfo.blinkCount; i++)
            {
                context.enemyInfo.sprite.color = context.hitInfo.blinkColors[(int)EnemyHitInfo.EBlinkType.OnBlink];
                yield return context.hitInfo.waitNextBlink;
                context.enemyInfo.sprite.color = context.hitInfo.blinkColors[(int)EnemyHitInfo.EBlinkType.OffBlink];
                yield return context.hitInfo.waitNextBlink;
            }
            context.hitInfo.stillBlink = false;
        }
    }
}
