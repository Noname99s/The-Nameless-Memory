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
    enum ESkillAni
    {
        Notify,
        SkillAttack,
    }
    [SerializeField] Animator animator;
    [SerializeField] Collider2D attackCollider;
    [SerializeField]float interval = 0, attackAniDuration = 0;
    bool isExecute = false;
    EExplosionSkill skillState = EExplosionSkill.Delay;
    private void OnEnable()
    {
        interval = 0;
        skillState = EExplosionSkill.Delay;
        attackCollider.enabled = false;
    }
    private void OnDisable()
    {
        isExecute = false;
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
        animator.SetTrigger(ESkillAni.Notify.ToString());

    }
    private void DoExplosion()
    {
        if (skillState != EExplosionSkill.Attack)
            return;

        skillState = EExplosionSkill.AttackEnd;
        animator.SetTrigger(ESkillAni.SkillAttack.ToString());
        attackCollider.enabled = true;
    }
    private void WaitDeactive()
    {
        if (skillState != EExplosionSkill.AttackEnd) 
            return;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(ESkillAni.SkillAttack.ToString()))
            return;

        attackAniDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        interval += Time.deltaTime;

        if (interval < attackAniDuration)
            return;

        interval = 0;
        Notify(ESkillNotify.SkillEnd);
        ObjectPoolManager.Instance.EnqueueObj((int)EObjectType.Explosion, gameObject);
    }
}
