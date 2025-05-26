using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
using Orpul1.Object;

[CreateAssetMenu(fileName = "SpawnExplosionCollider",menuName = "BehaviorNodes/Action/SpawnExplosionCollider")]
public class SpawnExplosionCollider : ActionScript
{
    public MirroredBlackboard blackboard;

    int[] indexs;
    [SerializeField] GameObject prefab;
    [SerializeField] int spawnCount;

    public override void Init()
    {
        ObjectPoolManager.Instance.SetPool((int)EObjectType.Explosion, prefab);
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
            ObjectPoolManager.Instance.GetObject<GameObject>((int)EObjectType.Explosion);
        }
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }

    private void SetLimitCount()
    {
       spawnCount = Mathf.Clamp(spawnCount, 0, blackboard.btComponent.skillContexts[(int)EBossSkill.Skill1].spawnPoints.Length);
    }
    private void SetRandomSpawnPoints()
    {
        indexs = new int[spawnCount];
        for (int i = 0; i < spawnCount; i++)
            indexs[i] = i;
        for (int i = 0; i < spawnCount; i++)
        {
            int shuffleIndex = Random.Range(0, spawnCount);
            int temp = indexs[i];
            indexs[i] = shuffleIndex;
            indexs[shuffleIndex] = temp;
        }
    }
}
