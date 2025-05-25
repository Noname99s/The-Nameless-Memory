using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "SpawnProjecttile",menuName = "BehaviorNodes/Action/SpawnProjecttile")]
public class SpawnProjecttile : ActionScript
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
