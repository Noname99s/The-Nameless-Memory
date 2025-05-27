using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOBehaviorTree
{
    public class DecoratorNode : LeafNode
    {
        public BehaviorNode childNode;
        [SerializeField] public Decorator Decorator;
        public override BehaviorNode Clone()
        {
            DecoratorNode node = Instantiate(this);
            node.childNode = childNode.Clone();
            return node;
        }
        public override void Init()
        {
            base.Init();
            childNode.Init();
            Decorator.Initialize();
        }
        public override NodeState Process()
        {
            if (!Decorator.Condition())
            {
                AbrotChildNode();
                nodeState = NodeState.Failure;
                return nodeState;
            }
            nodeState = childNode.Process();
            return nodeState;
        }
        private void AbrotChildNode()
        {
            if (childNode.nodeState != NodeState.Running) return;
            childNode.AbortNode();
        }
    }
}