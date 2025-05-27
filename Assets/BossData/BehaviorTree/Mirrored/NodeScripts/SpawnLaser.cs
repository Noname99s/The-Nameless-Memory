using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;

[CreateAssetMenu(fileName = "SpawnLaser",menuName = "BehaviorNodes/Action/SpawnLaser")]
public class SpawnLaser : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] GameObject prefab;
    [SerializeField] float laserDuration;
    public override void Init()
    {
        //ObjectPoolManager.Instance.SetPool((int)EObjectType.Laser, prefab);
    }
    public override NodeState OnStart()
    {
        //TODO : 레이저 duration 설정
        //ObjectPoolManager.Instance.SetPool((int)EObjectType.Laser, prefab);
        //ObjectPoolManager.Instance.SetPool((int)EObjectType.Laser, prefab);
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
