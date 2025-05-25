using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "SetUseSkill",menuName = "BehaviorNodes/Action/SetUseSkill")]
public class SetUseSkill : ActionScript
{
    public MirroredBlackboard blackboard;
    public EBossSkill bossSkill;
    public bool isUseSkil = false;
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
