using System;
using System.Collections.Generic;
using UnityEngine;

namespace SOBehaviorTree
{
    public abstract class ControlFlowNode : BehaviorNode
    {
        public List<BehaviorNode> childNodeList = new List<BehaviorNode>();
        protected int curIndex;
        public override void Init()
        {
            curIndex = 0;
            for(int i=0; i< childNodeList.Count; i++)
            {
                childNodeList[i].Init();
            }
        }
        public override void AbortNode()
        {
            childNodeList[curIndex].AbortNode();
        }
        public override BehaviorNode Clone()
        {
            ControlFlowNode node = Instantiate(this);
            for(int i=0; i<childNodeList.Count; i++)
            {
                node.childNodeList.Add(childNodeList[i].Clone());
            }
            return node;
        }
    }
}
