using SOBehaviorTree;
using UnityEngine;

namespace SOBehaviorTree
{
    public class PanelNode : BehaviorNode
    {
        public BehaviorTree panel;
        public override void Init()
        {
            if (panel == null) return;
            panel.Initialize();
        }
        public override NodeState Process()
        {
            if (panel == null) return NodeState.Failure;
            nodeState = panel.root.Process();
            return nodeState;
        }
        public override void SetEditorNodeName()
        {
            if (panel == null)
                description = "Panel";
            else
                description = panel.name;
        }
    }
}