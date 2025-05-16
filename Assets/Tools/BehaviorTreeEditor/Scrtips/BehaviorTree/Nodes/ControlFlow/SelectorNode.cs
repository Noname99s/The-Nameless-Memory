using UnityEngine;
using UnityEngine.XR;

namespace SOBehaviorTree
{
    [CreateAssetMenu(fileName = "SelectorNode", menuName = "BehaviorTree/ControlFlow/SelectorNode")]
    public class SelectorNode : ControlFlowNode
    {
        public override NodeState Process()
        {
            if (curIndex >= childNodeList.Count)
                curIndex = 0;
            NodeState nodeState = childNodeList[curIndex].Process();

            if (nodeState == NodeState.Failure)
                curIndex++;

            else if (nodeState == NodeState.Success)
                curIndex = 0;

            return nodeState;
        }
    }
}