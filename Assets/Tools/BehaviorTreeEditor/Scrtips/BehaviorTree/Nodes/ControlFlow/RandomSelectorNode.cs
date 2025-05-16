using SOBehaviorTree;
using UnityEngine;

public class RandomSelectorNode : ControlFlowNode
{

    public override void Init() 
    {
        curIndex = Random.Range(0, childNodeList.Count);
    }
    public override NodeState Process()
    {
        nodeState = childNodeList[curIndex].Process();
        if (nodeState == NodeState.Running) return NodeState.Running;
        curIndex = Random.Range(0, childNodeList.Count);
        
        return nodeState;
    }
}
