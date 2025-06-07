using System;
using UnityEngine;

namespace Orpul1.Enemy.Weapon
{
    public class ArrowWeapon : EnemyWeapon, ISetDataAble<Vector3, int>, ISetDataAble<Action<GameObject>>
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

        [SerializeField] Vector2 shootDir = Vector2.zero;
        [SerializeField] float angle = 0;
        Vector2 bezirP2, bezirP3;
        [SerializeField] bool isExecute = false;

        public override void Execute()
        {
            isExecute = true;
            executeTime = Time.time + deactiveTime;
        }

        public override void ResetValue()
        {
            isExecute = false;
            executeTime = 0;
            ISetDataAble<int> attack = arrowAttack as ISetDataAble<int>;
            if (attack == null) return;
            attack.SetValue(damage);
        }

        public void SetValue(Vector3 dir, int damage)
        {
            this.shootDir = dir;
            SetAngle();
        }
        private void SetAngle()
        {
            if (shootDir.y == 0) shootDir.y = 0.001f;
            angle = Mathf.Atan2(shootDir.x, shootDir.y) * Mathf.Rad2Deg * -1;
            //if (shootDir.y < 0) angle += 180;
            transform.eulerAngles = Vector3.forward * angle;
        }
        public void SetValue(Action<GameObject> enqueueAction)
        {
            this.enqueueAction = enqueueAction;
        }

        private void Update()
        {
            if (!isExecute) return;
            DeactiveObject();
        }
        private void FixedUpdate()
        {
            if (!isExecute) return;
            rigid.linearVelocity = shootDir * shootSpeed;
        }
        private void DeactiveObject()
        {
            if (Time.time < executeTime) return;
            enqueueAction(gameObject);
        }
        
    }
}