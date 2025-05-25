using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "SpawnExplosionCollider",menuName = "BehaviorNodes/Action/SpawnExplosionCollider")]
public class SpawnExplosionCollider : ActionScript
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
