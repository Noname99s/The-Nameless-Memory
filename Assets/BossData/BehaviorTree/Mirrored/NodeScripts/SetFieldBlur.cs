using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "SetFieldBlur",menuName = "BehaviorNodes/Action/SetFieldBlur")]
public class SetFieldBlur : ActionScript
{
    public MirroredBlackboard blackboard;
    public bool isBlur = false;
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
