using System;
using Orpul1.Boss;
using Orpul1.Boss.Mirrored;
using Orpul1.Object;
using UnityEngine;

public class ShockWaveSkill : BossSkill
{
    //TODO: FireExplosion.cs 코드와 완전 동일하기 때문에 코드를 하나로 통합할 것
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
    enum ESkillAniName
    {
        Idle,
        NotifyShockWave,
        ShockWave,
    }
    [SerializeField] Animator animator;
    [SerializeField] float interval = 0, attackAniDuration = 0;
    bool isExecute = false;
    EExplosionSkill skillState = EExplosionSkill.Delay;
    private void OnEnable()
    {
    }
    private void OnDisable()
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
    }
    private void WaitDeactive()
    {
        if (skillState != EExplosionSkill.AttackEnd)
            return;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(ESkillAniName.ShockWave.ToString()))
            return;

        attackAniDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        interval += Time.deltaTime;

        if (interval < attackAniDuration)
            return;

        interval = 0;
        enqueueAction(gameObject);
    }

    public override void SetValue(Action<GameObject> enqueueAction)
    {
        this.enqueueAction = enqueueAction;
    }

    public override void ResetValue()
    {
        isExecute = false;
        skillState = EExplosionSkill.Delay;
        interval = 0;
    }
}