using System;
using System.Collections;
using UnityEngine;

public class MonsterSpawnEffect : BossSkill
{
    enum EMonsterSpawnEffectAni
    {
        SpawnEffect,
    }
    [SerializeField] Animator animator;

    public override void Execute()
    {
        StartCoroutine("ShowEffect");
    }

    public override void ResetValue()
    {
    }

    public override void SetValue(Action<GameObject> enqueueAction)
    {
        this.enqueueAction = enqueueAction;
    }
    IEnumerator ShowEffect()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(EMonsterSpawnEffectAni.SpawnEffect.ToString()))
            yield return null;
        
        float aniDuration = Time.time + animator.GetCurrentAnimatorStateInfo(0).length;
        
        while(Time.time<aniDuration)
            yield return null;
        enqueueAction(gameObject);
    }
    
}
