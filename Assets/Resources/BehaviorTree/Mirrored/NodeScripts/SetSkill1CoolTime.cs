using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "SetSkill1CoolTime",menuName = "BehaviorNodes/Action/SetSkill1CoolTime")]
public class SetSkill1CoolTime : ActionScript
{
    public MirroredBlackboard blackboard;
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        return NodeState.Running;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
