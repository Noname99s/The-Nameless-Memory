using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;
using Orpul1.Boss.Mirrored;
using Orpul1;

[CreateAssetMenu(fileName = "SpawnProjecttile",menuName = "BehaviorNodes/Action/SpawnProjecttile")]
public class SpawnProjecttile : ActionScript
{
    public MirroredBlackboard blackboard;
    MirroredSkillInfo skillInfo;
    public override void Init()
    {
        skillInfo = blackboard.btComponent.bossSkillInfo;
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        skillInfo.bossSkillFactories[(int)EBossSkill.Skill2].Create();
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
