using System;
using UnityEngine;
using Orpul1.Boss;
using System.Collections;
using Orpul1.Boss.Sound;
namespace Orpul1.Boss.Mirrored
{
    public class GazeofResentmentSkill : BossSkill
    {
        enum ESkillAniName
        {
            ShootLaser
        }
        [SerializeField] Animator animator;
        WaitForSeconds coDeactiveTime;
        bool isExcute = false;


        private void Update()
        {
            if (!isExcute) return;
        }
        public override void Execute()
        {
            isExcute = true;
            sound.PlaySound((int)EBossSkillSound.Skill4_FullAttack);
            SetAttackDamage(attackCollider);
            StartCoroutine("UseSkill");
        }
        private void SetAttackDamage(ISetDataAble<int> attackCollider)
        { 
            attackCollider.SetValue(damage);
        }
        IEnumerator UseSkill()
        {
            animator.SetTrigger(EMirroredSkillAniParam.SkillAttack.ToString());
            while (!IsAniStart())
            {
                yield return null;
            }
            yield return coDeactiveTime;
            enqueueAction(gameObject);
        }
        private bool IsAniStart()
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(ESkillAniName.ShootLaser.ToString())) return false;
            coDeactiveTime = new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            return true;
        }
        public override void ResetValue()
        {
            isExcute = false;
        }

        public override void SetValue(Action<GameObject> enqueueAction)
        {
            this.enqueueAction = enqueueAction;
        }
        private void OnDisable()
        {
            ResetValue();
        }
    }
}