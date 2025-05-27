using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
using Orpul1.Object;

[CreateAssetMenu(fileName = "SpawnExplosionCollider",menuName = "BehaviorNodes/Action/SpawnExplosionCollider")]
public class SpawnExplosionCollider : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] int spawnCount;

    public override void Init()
    {
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            blackboard.btComponent.bossSkillFactories[(int)EBossSkill.Skill1].Create();
        }

        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }

}
