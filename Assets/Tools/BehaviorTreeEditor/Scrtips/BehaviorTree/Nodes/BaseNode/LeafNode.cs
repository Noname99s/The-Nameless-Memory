using System;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace SOBehaviorTree
{
    public class LeafNode : BehaviorNode
    {
        public string methodName = "";
        public string path;
        public ActionNode actionNode;
        [SerializeField] public string scriptPath;
        [SerializeField] public string assetPath;
        protected enum EActionState
        {
            OnStart,
            OnUpdate,
            OnExit
        }
        protected EActionState actionType = EActionState.OnStart;
        public override void SetEditorNodeName()
        {
            description = methodName;
        }
        public override void RemoveNode()
        {
            if (!File.Exists(scriptPath)) return;
            if (!File.Exists(assetPath)) return;
            File.Delete(scriptPath);
            File.Delete(assetPath);
            AssetDatabase.Refresh();
        }
    }
}
