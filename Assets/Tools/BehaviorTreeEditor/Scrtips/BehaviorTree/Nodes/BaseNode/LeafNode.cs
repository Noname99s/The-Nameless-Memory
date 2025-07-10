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
        [HideInInspector] public string path;
        [HideInInspector] public ActionNode actionNode;
        [HideInInspector] public string scriptPath;
        [HideInInspector] public string assetPath;
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
#if UNITY_EDITOR
        public override void RemoveNode()
        {
            if (!File.Exists(scriptPath)) return;
            if (!File.Exists(assetPath)) return;
            File.Delete(scriptPath);
            File.Delete(assetPath);
            AssetDatabase.Refresh();
        }
#endif
    }
}
