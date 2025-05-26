using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;

[CreateAssetMenu(fileName = "SpawnShockwave",menuName = "BehaviorNodes/Action/SpawnShockwave")]
public class SpawnShockwave : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] GameObject prefab;
    public override void Init()
    {
        ObjectPoolManager.Instance.SetPool((int)EObjectType.ShockWave, prefab);
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        ObjectPoolManager.Instance.GetObject<GameObject>((int)EObjectType.ShockWave);
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
