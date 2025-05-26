using UnityEngine;

namespace SOBehaviorTree
{
    [CreateAssetMenu(fileName = "SequenceNode", menuName = "BehaviorTree/ControlFlow/SequenceNode")]
    public class SequenceNode : ControlFlowNode
    {
        public override NodeState Process()
        {
            if (curIndex >= childNodeList.Count)
            {
                curIndex = 0;
                return NodeState.Success;
            }
            nodeState = NodeState.Running;
            nodeState = childNodeList[curIndex].Process();

            if (nodeState == NodeState.Success)
                curIndex++;

            else if (nodeState == NodeState.Failure)
            {
                curIndex = 0;
                return NodeState.Failure;
            }
            return NodeState.Running;
        }
    }
}
