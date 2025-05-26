using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "SpawnMonster",menuName = "BehaviorNodes/Action/SpawnMonster")]
public class SpawnMonster : ActionScript
{
    public MirroredBlackboard blackboard;

    public override void Init()
    {
        
    }
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
