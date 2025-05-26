using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
using Orpul1.Object;

[CreateAssetMenu(fileName = "SpawnExplosionCollider",menuName = "BehaviorNodes/Action/SpawnExplosionCollider")]
public class SpawnExplosionCollider : ActionScript
{
    public MirroredBlackboard blackboard;

    [SerializeField] GameObject prefab;
    [SerializeField] int spawnCount;
    [SerializeField] Transform[] spawnPosis;
    [SerializeField] int[] indexs;

    public override void Init()
    {
        ObjectPoolManager.Instance.SetPool((int)EObjectType.Explosion, prefab);
        spawnPosis = blackboard.btComponent.skillContexts[(int)EBossSkill.Skill1].spawnPoints;
        indexs = new int[spawnPosis.Length];
    }
    public override NodeState OnStart()
    {
        SetLimitCount();
        SetRandomSpawnPoints();
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        Debug.Log("ExplosionCollider ½ÇÇà");
        for (int i = 0; i < spawnCount; i++)
        {
            BossSkill skill = ObjectPoolManager.Instance.GetObject<BossSkill>((int)EObjectType.Explosion);
            skill.transform.position = spawnPosis[indexs[i]].position;
            skill.Execute();
        }
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }

    private void SetLimitCount()
    {
       spawnCount = Mathf.Clamp(spawnCount, 0, spawnPosis.Length);
    }
    private void SetRandomSpawnPoints()
    {
        for (int i = 0; i < spawnPosis.Length; i++)
            indexs[i] = i;
        for (int i = 0; i < spawnPosis.Length; i++)
        {
            int shuffleIndex = Random.Range(0, spawnPosis.Length);
            int temp = indexs[i];
            indexs[i] = indexs[shuffleIndex];
            indexs[shuffleIndex] = temp;
        }
    }
}
