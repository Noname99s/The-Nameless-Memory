using System.Collections;
using System.Collections.Generic;
using SOBehaviorTree;
using UnityEngine;

public class ReturnFailerNode : LeafNode
{
    public override NodeState Process()
    {
        return NodeState.Failure;
    }
}
