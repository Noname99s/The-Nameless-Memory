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
            return panel.root.Process();
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