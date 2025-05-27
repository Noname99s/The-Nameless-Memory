using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "DebugPanelEnd",menuName = "BehaviorNodes/Action/DebugPanelEnd")]
public class DebugPanelEnd : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] string debugStr;

    public override void Init()
    {
        
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        Debug.Log(debugStr);
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
