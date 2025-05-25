using UnityEngine;

namespace SOBehaviorTree
{
    [CreateAssetMenu(fileName = "Root",menuName = "BehaviorNode/Root")]
    public class RootNode : BehaviorNode
    {
        public BehaviorNode child;
        public override void Init()
        {
            child.Init();
        }

        public override NodeState Process()
        {
            return child.Process();
        }
        public override BehaviorNode Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}