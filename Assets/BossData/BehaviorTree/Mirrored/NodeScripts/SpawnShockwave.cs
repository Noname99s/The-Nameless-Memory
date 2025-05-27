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
    [SerializeField] Transform trPlayer;

    public override void Init()
    {
        if (!Application.isPlaying) return;
        ObjectPoolManager.Instance.SetPool((int)EObjectType.ShockWave, prefab);
        trPlayer = blackboard.btComponent.trPlayer;
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        BossSkill skill = ObjectPoolManager.Instance.GetObject<BossSkill>((int)EObjectType.ShockWave);
        skill.transform.position = trPlayer.position;
        skill.Execute();
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
