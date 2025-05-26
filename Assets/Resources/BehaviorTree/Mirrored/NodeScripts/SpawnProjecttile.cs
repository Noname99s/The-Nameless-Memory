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
    [SerializeField] GameObject prefab;
    [SerializeField] Transform[] startPosis;
    [SerializeField] Transform[] arrivePosis;

    public override void Init()
    {
        ObjectPoolManager.Instance.SetPool((int)EObjectType.EnergyBall, prefab);
        startPosis = blackboard.btComponent.skillContexts[(int)EBossSkill.Skill2].spawnPoints;
        arrivePosis = blackboard.btComponent.handPosis;
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        for (int i = 0; i < startPosis.Length; i++)
        {
            SpawnEnergyBall(startPosis[i].position, arrivePosis[i].position);
        }
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
    private void SpawnEnergyBall(Vector2 startPosi,Vector2 arrivePosis)
    {
        BossSkill skill = ObjectPoolManager.Instance.GetObject<BossSkill>((int)EObjectType.EnergyBall);
        skill.transform.position = startPosi;
        ISetDataAble setValue = skill as ISetDataAble;
        if (setValue == null) return;
        setValue.SetValue(arrivePosis);
        skill.Execute();
    }
}
