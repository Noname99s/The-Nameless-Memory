using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "ActSkill1Ani",menuName = "BehaviorNodes/Action/ActSkill1Ani")]
public class ActSkill1Ani : ActionScript
{
    public MirroredBlackboard blackboard;
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
