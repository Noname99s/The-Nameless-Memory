using UnityEngine;

namespace SOBehaviorTree
{
    [CreateAssetMenu(fileName = "ParallelNode",menuName = "BehaviorTree/ControlFlow/ParallelNode")]
    public class ParallelNode : ControlFlowNode
    {
        public override NodeState Process()
        {

            nodeState = NodeState.Success;
            for (int i = 0; i < childNodeList.Count; i++)
            {
                switch (childNodeList[i].Process())
                {
                    case NodeState.Success:
                        break;
                    case NodeState.Failure:
                        nodeState = NodeState.Failure;
                        break;
                }
            }
            return nodeState;
        }
    }
}