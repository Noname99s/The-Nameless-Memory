using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
[CreateAssetMenu(fileName = "SetIdleState",menuName = "BehaviorNodes/Action/SetIdleState")]
public class SetIdleState : ActionScript
{
    public MirroredBlackboard blackboard;
    public override NodeState OnStart()
    {
        blackboard.btComponent.bossState = EBossState.Idle;
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
