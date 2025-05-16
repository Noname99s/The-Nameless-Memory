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
        }
        public override NodeState Process()
        {
            if (!Decorator.Condition())
            {
                Debug.Log("decorator Child Node Abort");
                AbrotChildNode();
                return NodeState.Failure;
            }
            return childNode.Process();
        }
        private void AbrotChildNode()
        {
            if (childNode.nodeState != NodeState.Running) return;
            childNode.AbortNode();
        }
    }
}