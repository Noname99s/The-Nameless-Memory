using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOBehaviorTree
{
    public class ReturnSuccessNode : LeafNode
    {
        public override NodeState Process()
        {
            return NodeState.Success;
        }
    }
}
