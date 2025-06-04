using System;
using Orpul1.Boss;
using Orpul1.Boss.Mirrored;
using Orpul1.Object;
using UnityEngine;

public class FireExplosionSkill : BossSkill
{
    enum EExplosionSkill
    {
        Delay,
        Attack,
        AttackEnd,
        End,
    }
    enum ESkillState
    {
        NotifyAttack,
        SkillAttack,
    }
    [SerializeField] Animator animator;
    [SerializeField]float interval = 0, attackAniDuration = 0;
    bool isExecute = false;
    EExplosionSkill skillState = EExplosionSkill.Delay;
    private void OnEnable()
    {
        ResetValue();
    }
    private void Update()
    {
        if (!isExecute)
            return;
        if (!IsExplosion()) 
            return;

        DoExplosion();
        WaitDeactive();
    }
    public override void Execute()
    {
        base.Execute();
        isExecute = true;
        DoExplosionSign();
    }
    private bool IsExplosion()
    {
        if (skillState == EExplosionSkill.AttackEnd) return true;
            interval += Time.deltaTime;
        if (interval < delayToAttack)
            return false;

        interval = 0;

        return true;
    }
    private void DoExplosionSign()
    {
        if (skillState != EExplosionSkill.Delay) 
            return;

        skillState = EExplosionSkill.Attack;
        animator.SetTrigger(EMirroredSkillAniParam.Notify.ToString());

    }
    private void DoExplosion()
    {
        if (skillState != EExplosionSkill.Attack)
            return;

        skillState = EExplosionSkill.AttackEnd;
        animator.SetTrigger(EMirroredSkillAniParam.SkillAttack.ToString());
        attackCollider.enabled = true;
    }
    private void WaitDeactive()
    {
        if (skillState != EExplosionSkill.AttackEnd) 
            return;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(EMirroredSkillAniParam.SkillAttack.ToString()))
            return;

        attackAniDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        interval += Time.deltaTime;

        if (interval < attackAniDuration)
            return;

        interval = 0;
        enqueueAction(gameObject);
    }

    public override void SetValue(Action<GameObject> enequeueObj)
    {
        this.enqueueAction = enequeueObj;
    }

    public override void ResetValue()
    {
        isExecute = false;
        attackCollider.enabled = false;
        skillState = EExplosionSkill.Delay;
        interval = 0;
    }
}
