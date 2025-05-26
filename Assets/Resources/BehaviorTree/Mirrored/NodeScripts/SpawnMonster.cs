using UnityEngine;
using SOBehaviorTree;
using Orpul1.Object;
using Orpul1;

[CreateAssetMenu(fileName = "SpawnMonster",menuName = "BehaviorNodes/Action/SpawnMonster")]
public class SpawnMonster : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] int spawnCount;
    public override void Init()
    {
        for (int i = (int)EObjectType.Monster1; i < (int)EObjectType.Monster3; i++)
        {
            ObjectPoolManager.Instance.SetPool(i, prefabs[i]);
        }
    }
    public override NodeState OnStart()
    {
       
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        int randIndex = 0;

        for (int i = 0; i < spawnCount; i++)
        {
            randIndex = Random.Range((int)EObjectType.Monster1, (int)EObjectType.Monster3);
            ISubject subject = ObjectPoolManager.Instance.GetObject<ISubject>(randIndex);
            subject.AddObserver(blackboard.btComponent.bossObserver);
        }
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
