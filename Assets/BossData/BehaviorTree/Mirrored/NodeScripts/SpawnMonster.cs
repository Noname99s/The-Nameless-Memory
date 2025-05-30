using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;
using Orpul1;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "SpawnMonster",menuName = "BehaviorNodes/Action/SpawnMonster")]
public class SpawnMonster : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] GameObject[] prefabs;
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
        blackboard.btComponent.bossSkillFactories[(int)EBossSkill.Skill6].Create();
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
