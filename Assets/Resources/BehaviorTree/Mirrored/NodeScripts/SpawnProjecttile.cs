using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;

[CreateAssetMenu(fileName = "SpawnProjecttile",menuName = "BehaviorNodes/Action/SpawnProjecttile")]
public class SpawnProjecttile : ActionScript
{
    public MirroredBlackboard blackboard;
    public GameObject prefab;
    public override void Init()
    {
        ObjectPoolManager.Instance.SetPool((int)EObjectType.EnergyBall, prefab);
    }
    public override NodeState OnStart()
    {
        ObjectPoolManager.Instance.GetObject<GameObject>((int)EObjectType.EnergyBall);
        ObjectPoolManager.Instance.GetObject<GameObject>((int)EObjectType.EnergyBall);
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
