using System;
using System.Collections;
using Orpul1.Enemy.State;
using Orpul1.State;
using UnityEngine;
using UnityEngine.Events;
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
    public class EnemyController : MonoBehaviour, IResetAble, IDamageAble, IknockbackAble, IDieAble, ISetDataAble<Action<GameObject>, int>
    {
        [SerializeField] StateController stateController;
        [SerializeField] EnemyContext context;
        [SerializeField] EEnemyState startState;
        [SerializeField] FieldOfView fieldOfView;
        [SerializeField] EnemyRayInfo enemyRay;
        [SerializeField] GameObject attackCollider;
        IDetectTargetAble enemySight;
        Vector3 scale = Vector3.zero;

        [ContextMenu("SetDebugScale")]
        private void SetScale()
        {
            scale = transform.localScale;
            context.enemyInfo.originScale = transform.localScale;
        }
        private void Awake()
        {
            enemySight = fieldOfView;
            context.enemyInfo.setSight = fieldOfView;
            context.Initialize();
            context.enemyInfo.flipX = FlipX;
            context.dieInfo.destroyAction = DeactiveObject;
            SetDamage();
            SetScale();
        }
        private void SetDamage()
        {
            if (attackCollider == null) return;
            attackCollider.GetComponent<ISetDataAble<int>>().SetValue(context.attackInfo.damage);
        }

        private void Start()
        {
            stateController.TransitionState((int)startState);
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
            context.enemyInfo.isGround = enemyRay.hit.transform != null;
        }
        private RaycastHit2D SetRay(Ray2D ray, Vector2 rayShootPosi, Vector2 dir, float distance, int detectLayer)
        {
            rayShootPosi.x *= scale.x;
            ray.origin = (Vector2)transform.position + rayShootPosi;
            ray.direction = dir;
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
            return Physics2D.Raycast(ray.origin, ray.direction, distance, detectLayer);
        }
        private void LookForward()
        {
            Transform target = enemySight.DetectTarget();
            if (target == null)
            {
                context.chaseInfo.isFindChaseTarget = false;
                return;
            }

            context.chaseInfo.isFindChaseTarget = true;
            context.chaseInfo.chaseTarget = target;
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
        private void DeactiveObject()
        {
            gameObject.SetActive(false);
        }
        public void ResetValue()
        {
            stateController.TransitionState((int)startState);
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
            dir *= context.hitInfo.knockBackPower;

            context.enemyInfo.rigid.AddForce(dir, ForceMode2D.Impulse);
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

        public void Die()
        {
            context.dieInfo.deactiveAction?.Invoke(gameObject);
        }
    }
}
