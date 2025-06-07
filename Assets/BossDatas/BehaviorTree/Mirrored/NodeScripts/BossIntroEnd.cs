using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "BossIntroEnd",menuName = "BehaviorNodes/Action/BossIntroEnd")]
public class BossIntroEnd : ActionScript
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
        blackboard.btComponent.bossInfo.shouldIntro = false;
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
