using System;
using UnityEngine;

namespace Orpul1.Enemy.Weapon
{
    public class ArrowWeapon : EnemyWeapon, ISetDataAble<Vector3, float>, ISetDataAble<Action<GameObject>>
    {
        enum EArrowAniParam
        {
            Rotate,
        }

        [SerializeField] SpriteRenderer sprite;
        [SerializeField] Animator animator;
        [SerializeField] Rigidbody2D rigid;
        [SerializeField] CharacterAttack arrowAttack;
        [SerializeField] Vector2 targetPosiBezierGap;
        [SerializeField] float deactiveTime;
        [SerializeField] float shootSpeed;
        [SerializeField] float executeTime = 0;
        Action<GameObject> enqueueAction;

        Vector2 targetPosi = Vector2.zero;
        [SerializeField] Vector2 dir = Vector2.zero;
        [SerializeField] float angle = 0;
        Vector2 bezirP2, bezirP3;
        [SerializeField] bool isExecute = false;

        //TODO : 베이저 곡선 적용


        private void Awake()
        {
            ISetDataAble<float> attack = arrowAttack as ISetDataAble<float>;
            if (attack == null) return;
            attack.SetValue(damage);
        }
        public override void Execute()
        {
            isExecute = true;
            executeTime += Time.time + deactiveTime;
        }

        public override void ResetValue()
        {
            isExecute = false;
            executeTime = 0;
        }

        public void SetValue(Vector3 target, float damage)
        {

            targetPosi = target;
            dir = (target - transform.position).normalized;
            SetAngle();

            this.damage = damage;
            SetBezir();
        }
        private void SetAngle()
        {
            if (dir.y == 0) dir.y = 0.001f;
            angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg * -1;
            if (dir.y < 0) angle -= 180;
            transform.eulerAngles = Vector3.forward * angle;
        }
        private void SetBezir()
        {
            bezirP2 = (Vector2)transform.position + targetPosiBezierGap;
            bezirP3 = (Vector2)transform.position;
            bezirP3.x -= targetPosiBezierGap.x;
            bezirP3.y += targetPosiBezierGap.y;
        }
        public void SetValue(Action<GameObject> enqueueAction)
        {
            this.enqueueAction = enqueueAction;
        }

        private void Update()
        {
            if (!isExecute) return;
            //MoveBezier();
            DeactiveObject();
        }
        private void FixedUpdate()
        {
            if (!isExecute) return;
            rigid.linearVelocity = dir * shootSpeed;
        }
        private void MoveBezier()
        {
            dir = Bezier(transform.position, bezirP2, bezirP3, targetPosi, Time.deltaTime).normalized;
        }
        private void DeactiveObject()
        {
            if (Time.time < executeTime) return;
            enqueueAction(gameObject);
        }
        private Vector3 Bezier(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            Vector3 m0 = Vector3.Lerp(p1, p2, t);
            Vector3 m1 = Vector3.Lerp(p2, p3, t);
            Vector3 m2 = Vector3.Lerp(p3, p4, t);

            Vector3 b0 = Vector3.Lerp(m0, m1, t);
            Vector3 b1 = Vector3.Lerp(m1, m2, t);
            return Vector3.Lerp(b0, b1, t);
        }

        
    }
}