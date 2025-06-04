using System;
using System.Collections;
using Orpul1.Enemy.State;
using Orpul1.State;
using UnityEngine;
namespace Orpul1.Enemy
{
    [Serializable]
    public class EnemyRayInfo
    {
        public Ray2D ray;
        public RaycastHit2D hit;
        public Vector3 groundRayPos;
        public LayerMask detectLayer;
        public float distance;
    }
    public class EnemyController : MonoBehaviour, IResetAble, IDamageAble,IknockbackAble, ISetDataAble<Action<GameObject>,int>
    {
        [SerializeField] StateController stateController;
        [SerializeField] EnemyContext context;
        [SerializeField] EEnemyState state;
        [SerializeField] FieldOfView fieldOfView;
        [SerializeField] EnemyRayInfo enemyRay;
        [SerializeField] CharacterAttack attackCollider;
        ISightFindAble enemySight;
        Vector3 scale = Vector3.zero;

        [ContextMenu("SetDebugScale")]
        private void DebugSetScale()
        {
            scale = transform.localScale;
        }
        private void Awake()
        {
            enemySight = fieldOfView;
            context.enemyInfo.setSight = fieldOfView;
            context.Initialize();
            context.enemyInfo.flipX = FlipX;
            context.dieInfo.destroyAction = DestroyObject;
            SetDamage(attackCollider);
        }
        private void SetDamage(ISetDataAble<int> attackCollider)
        {
            if (attackCollider == null) return;
            attackCollider.SetValue(context.attackInfo.damage);
        }

        private void Start()
        {
            stateController.TransitionState((int)state);
        }
        void Update()
        {
            LookForward();
            OnGround();
        }
        private void OnGround()
        {
            enemyRay.hit = SetRay(enemyRay.ray, enemyRay.groundRayPos, Vector2.down, enemyRay.distance, enemyRay.detectLayer.value);
            Debug.DrawRay(enemyRay.ray.origin, enemyRay.ray.direction * enemyRay.distance, Color.green);
            context.enemyInfo.isGround = IsHitRay(enemyRay.hit);
        }
        private RaycastHit2D SetRay(Ray2D ray, Vector2 rayShootPosi, Vector2 dir, float distance, int detectLayer)
        {
            rayShootPosi.x *= scale.x;
            ray.origin = (Vector2)transform.position + rayShootPosi;
            ray.direction = dir;
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
            return Physics2D.Raycast(ray.origin, ray.direction, distance, detectLayer);
        }
        private bool IsHitRay(RaycastHit2D hit2D)
        {
            if (hit2D.transform == null) return false;

            return true;
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

            scale = context.enemyInfo.originScale;
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
            context.enemyInfo.enemyCollider.enabled = true;
        }

        public void SetValue(Action<GameObject> enqueueAction, int patrolType)
        {
            context.dieInfo.deactiveAction = enqueueAction;
            context.patrolInfo.patrolType = (EnemyPatrolInfo.EPatrolType)patrolType;
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
            context.enemyInfo.rigid.linearVelocity = Vector2.zero;
            dir.y = 1;

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
        private void OnDrawGizmos()
        {
            SetRay(enemyRay.ray, enemyRay.groundRayPos, Vector2.down, enemyRay.distance, enemyRay.detectLayer.value);
            Debug.DrawRay(enemyRay.ray.origin, enemyRay.ray.direction * enemyRay.distance, Color.green);
        }
    }
}
