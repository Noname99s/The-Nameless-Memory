using System;
using System.Text;
using UnityEngine;

namespace SOBehaviorTree
{
    public abstract class BehaviorNode : ScriptableObject
    {
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public string nodeName;
        [HideInInspector] public string description;
        public NodeState nodeState;
        
        public virtual void Init() { }
        public virtual NodeState Process()
        {
            return NodeState.Success;
        }
        public virtual void AbortNode() { }
        public virtual BehaviorNode Clone()
        {
            return Instantiate(this);
        }
        public virtual void SetEditorNodeName()
        {
            description = name;
        }
        public virtual void RemoveNode() { }
    }
}
