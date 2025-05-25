using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "TestDebugA",menuName = "BehaviorNodes/Action/TestDebugA")]
public class TestDebugA : ActionScript
{
    public MirroredBlackboard blackboard;
    public override NodeState OnStart()
    {
        Debug.Log("Start A");
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        Debug.Log("Update A");
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        Debug.Log("Exit A");
        return NodeState.Success;
    }
}
