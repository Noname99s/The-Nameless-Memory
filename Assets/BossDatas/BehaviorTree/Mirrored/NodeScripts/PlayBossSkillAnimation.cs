using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "PlaySkill1Animation",menuName = "BehaviorNodes/Action/PlaySkill1Animation")]
public class PlayBossSkillAnimation : ActionScript
{
    public MirroredBlackboard blackboard;
    public EBossSkill bossAni;
    Animator animator;
    public override void Init()
    {
        animator = blackboard.btComponent.bossInfo.animator;
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        if (animator == null)
            return NodeState.Success;
        animator.SetTrigger(bossAni.ToString());
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
