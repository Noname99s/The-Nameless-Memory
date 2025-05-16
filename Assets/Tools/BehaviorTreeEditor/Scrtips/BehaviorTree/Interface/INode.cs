using System;
namespace SOBehaviorTree
{
    public interface INode
    {
        public void AddNode(INode node);
        public void AddActiveFunc(Func<NodeState> activate);
        public NodeState Execute();
    }
}
