using UnityEngine;

namespace SOBehaviorTree
{
    [CreateAssetMenu(fileName = "Root",menuName = "BehaviorNode/Root")]
    public class RootNode : BehaviorNode
    {
        public BehaviorNode child;
        [SerializeField] string panelName;
        public override void Init()
        {
            child.Init();
        }

        public override NodeState Process()
        {
            if(child == null)
            {
                Debug.LogError($"{panelName} child is Null");
                return NodeState.Failure;
            }
            nodeState = child.Process();
            return nodeState;
        }
        public override BehaviorNode Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}