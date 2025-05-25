using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "SetIdleState",menuName = "BehaviorNodes/Action/SetIdleState")]
public class SetIdleState : ActionScript
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
