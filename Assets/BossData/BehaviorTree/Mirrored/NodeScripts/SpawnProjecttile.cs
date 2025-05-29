using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;
using Orpul1.Boss.Mirrored;
using Orpul1;

[CreateAssetMenu(fileName = "SpawnProjecttile",menuName = "BehaviorNodes/Action/SpawnProjecttile")]
public class SpawnProjecttile : ActionScript
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
        blackboard.btComponent.bossSkillFactories[(int)EBossSkill.Skill2].Create();
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
