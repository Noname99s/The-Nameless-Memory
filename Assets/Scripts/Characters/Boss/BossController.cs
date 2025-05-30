using Orpul1;
using Orpul1.Boss.Mirrored;
using Orpul1.Object;
using SOBehaviorTree;
using UnityEngine;

public class BossController : MonoBehaviour,IFindTreeAble, IDamageAble
{
    [SerializeField] BehaviorTree tree;
    [SerializeField] MirroredBlackboard mirroredBlackboard;
    [SerializeField] MirroredBlackboardComponent mirroredBlackboardComponent;
    [SerializeField] GameObject skillEffectSpawner;

    public BehaviorTree GetBehaviorTree()
    {
        return tree;
    }

    void Start()
    {
        tree.root.Init();
        mirroredBlackboard.btComponent = mirroredBlackboardComponent;
    }

    void Update()
    {
        tree.root.Process();
    }

    public void OnDamage(int damage)
    {
        mirroredBlackboard.btComponent.hp -= damage;
    }
}
