using Orpul1.Object;
using SOBehaviorTree;
using UnityEngine;

public class BossController : MonoBehaviour,IFindTreeAble
{
    [SerializeField] BehaviorTree tree;
    [SerializeField] MirroredBlackboard mirroredBlackboard;
    [SerializeField] MirroredBlackboardComponent mirroredBlackboardComponent;

    public BehaviorTree GetBehaviorTree()
    {
        return tree;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ObjectPoolManager.Instance.SetPool((int)EObjectType.Explosion);
        tree.Initialize();
        mirroredBlackboard.btComponent = mirroredBlackboardComponent;

    }

    // Update is called once per frame
    void Update()
    {
        tree.root.Process();
    }
    private void OnDestroy()
    {
        ObjectPoolManager.Instance.ReleasePool(0);
    }
}
