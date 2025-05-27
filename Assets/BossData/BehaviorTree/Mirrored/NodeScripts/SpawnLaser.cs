using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "SpawnLaser",menuName = "BehaviorNodes/Action/SpawnLaser")]
public class SpawnLaser : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] GameObject prefab;
    [SerializeField] float laserDuration;
    public override void Init()
    {
    }
    public override NodeState OnStart()
    {
        //TODO : 레이저 duration 설정
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        blackboard.btComponent.bossSkillFactories[(int)EBossSkill.Skill4].Create();
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
