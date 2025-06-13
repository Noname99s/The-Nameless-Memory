using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
using DG.Tweening;

[CreateAssetMenu(fileName = "ClearEnemies",menuName = "BehaviorNodes/Action/ClearEnemies")]
public class ClearEnemies : ActionScript
{
    public MirroredBlackboard blackboard;
    Transform sliderEndPosi;
    IDeactiveAble deactiveEnemies;
    public override void Init()
    {
        deactiveEnemies = blackboard.btComponent.bossSkillInfo.bossSkillFactories[(int)EBossSkill.Skill6] as IDeactiveAble;
        sliderEndPosi = blackboard.btComponent.bossInfo.sliderMovePosis[0];
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        DeactiveEenemies();
        blackboard.btComponent.bossInfo.sliderUI.transform.DOLocalMove(sliderEndPosi.position, 1);
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
    private void DeactiveEenemies()
    {
        if (deactiveEnemies == null) return;
        deactiveEnemies.Deactive();
    }
}
