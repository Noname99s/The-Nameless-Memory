using Orpul1;
using Orpul1.Object;
using SOBehaviorTree;
using UnityEngine;

public class BossController : MonoBehaviour,IFindTreeAble, IObserver
{
    [SerializeField] BehaviorTree tree;
    [SerializeField] MirroredBlackboard mirroredBlackboard;
    [SerializeField] MirroredBlackboardComponent mirroredBlackboardComponent;

    public BehaviorTree GetBehaviorTree()
    {
        return tree;
    }

    void Start()
    {
        //tree.Initialize();
        tree.root.Init();
        mirroredBlackboardComponent.bossObserver = this;
        mirroredBlackboard.btComponent = mirroredBlackboardComponent;
    }

    // Update is called once per frame
    void Update()
    {
        tree.root.Process();
    }
    private void OnDestroy()
    {
        //ObjectPoolManager.Instance.ReleasePool(0);
    }

    public void Notify<T>(T t)
    {
        // TODO : 몬스터 수, 스킬 사용 여부 알림 받기
    }
}
