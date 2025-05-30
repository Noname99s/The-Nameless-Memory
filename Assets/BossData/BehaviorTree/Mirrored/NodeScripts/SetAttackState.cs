using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "SetAttackState",menuName = "BehaviorNodes/Action/SetAttackState")]
public class SetAttackState : ActionScript
{
    public MirroredBlackboard blackboard;
    public override NodeState OnStart()
    {
        blackboard.btComponent.bossInfo.bossState = EBossState.Attack;
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
