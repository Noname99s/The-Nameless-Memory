using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "SpawnShockwave",menuName = "BehaviorNodes/Action/SpawnShockwave")]
public class SpawnShockwave : ActionScript
{
    public MirroredBlackboard blackboard;

    public override void Init()
    {
        if (!Application.isPlaying) return;
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        blackboard.btComponent.bossSkillFactories[(int)EBossSkill.Skill3].Create();
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
