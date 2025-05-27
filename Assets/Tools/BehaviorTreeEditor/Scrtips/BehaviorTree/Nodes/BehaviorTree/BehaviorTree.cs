using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public enum NodeKeyType
{
    NodeScriptPathKey,
    NodeAssetPathKey,
    NodeClassNameKey,
    NodeType,
    NodeGuid
}
namespace SOBehaviorTree
{
    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject
    {
        public BehaviorNode root;
        public ScriptableObject blackboard;

        [HideInInspector] private string blackboardPath;
        [HideInInspector] public string blackboardName;
        [HideInInspector] private string treePath = "";
        public List<BehaviorNode> nodeList = new List<BehaviorNode>();

        public string TreePath { get { return treePath; } }
        public string BlackboardPath { get { return blackboardPath; } }
        public void Initialize()
        {
#if UNITY_EDITOR
            SetPath();
#endif
        }
        public void RootInit()
        {
            root.Init();
        }
#if UNITY_EDITOR

        public BehaviorNode CreateNode(Type type, string nodeName)
        {
            BehaviorNode node = SetNode(type, nodeName);
            nodeList.Add(node);

            bool isSuccess = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long localId);
            if (!isSuccess) return null;
            SaveNode(node, guid);

            return node;
        }

        public BehaviorNode CreateNode(Type nodeType, object nodeClass, string nodeName)
        {
            BehaviorNode node = SetNode(nodeType, nodeClass, nodeName);
            nodeList.Add(node);

            bool isSuccess = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long localId);
            if (!isSuccess) return null;
            SaveNode(node, guid);
            return node;
        }
        private void SetPath()
        {
            if (!string.IsNullOrEmpty(treePath)) return;
            bool isSuccess = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long localId);
            if (!isSuccess) return;
            treePath = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log(this.name);
            treePath = treePath.Substring(treePath.IndexOf("/") + 0);
            treePath = treePath.Replace(this.name + ".asset", "");
        }
      
        private BehaviorNode SetNode(Type type, string nodeName)
        {
            BehaviorNode node = ScriptableObject.CreateInstance(type) as BehaviorNode;
            node.nodeName = nodeName;
            node.name = nodeName;
            node.guid = GUID.Generate().ToString();
            return node;
        }
        private BehaviorNode SetNode(Type nodeType, object nodeClass, string nodeName)
        {
            
            switch (nodeType.Name)
            {
                case nameof(ActionNode):
                    {
                        ActionNode node = ScriptableObject.CreateInstance(nodeType) as ActionNode;
                        node = SetLeafNode(node, nodeName) as ActionNode;
                        node.actionScript = (ActionScript)nodeClass;
                        
                        return node;
                    }
                case nameof(DecoratorNode):
                    {
                        DecoratorNode node = ScriptableObject.CreateInstance(nodeType) as DecoratorNode;
                        node = SetLeafNode(node, nodeName) as DecoratorNode;
                        node.Decorator = (Decorator)nodeClass;

                        return node;
                    }
            }
            return null;
        }
        private LeafNode SetLeafNode(LeafNode node, string nodeName)
        {
            string scriptPath = EditorPrefs.GetString(NodeKeyType.NodeScriptPathKey.ToString(), null);
            string assetPath = EditorPrefs.GetString(NodeKeyType.NodeAssetPathKey.ToString(), null);
            if(string.IsNullOrEmpty(scriptPath)||string.IsNullOrEmpty(assetPath))
            {
                Debug.Log("isEditorPrefs Null!");
            }
            node.nodeName = nodeName;
            node.name = nodeName;
            node.guid = GUID.Generate().ToString();
            node.scriptPath = scriptPath;
            node.assetPath = assetPath;
            return node;
        }
        private void SaveNode(BehaviorNode node, string guid)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            AssetDatabase.AddObjectToAsset(node, path);
            AssetDatabase.SaveAssets();
        }

        public void SetBlackboardPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            blackboardPath = path;
        }

        public void DeleteNode(BehaviorNode node)
        {
            node.RemoveNode();
            nodeList.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
#endif
        public void AddChilde(BehaviorNode parent, BehaviorNode child)
        {
            if (parent is DecoratorNode decorNode)
                decorNode.childNode = child;
            else if (parent is RootNode rootNode)
                rootNode.child = child;
            else if (parent is ControlFlowNode cfNode)
                cfNode.childNodeList.Add(child);
            
        }

        public void RemoveChilde(BehaviorNode parent, BehaviorNode child)
        {
            if (parent is DecoratorNode decorNode)
                decorNode.childNode = null;

            else if (parent is RootNode rootNode)
                rootNode.child = null;

            else if (parent is ControlFlowNode cfNode)
                cfNode.childNodeList.Remove(child);
        }

        public List<BehaviorNode> GetChilds(BehaviorNode parent)
        {
            List<BehaviorNode> childList = new List<BehaviorNode> ();

            if (parent is DecoratorNode docorNode)
            {
                if (docorNode.childNode == null) return childList;
                childList.Add(docorNode.childNode);
            }
            else if (parent is RootNode rootNode)
            {
                if(rootNode.child == null) return childList;

                childList.Add(rootNode.child);
            }
            else if (parent is ControlFlowNode cfNode)
            {
                return cfNode.childNodeList;
            }
            return childList;
        }
        public BehaviorTree Clone()
        {
            BehaviorTree tree = Instantiate(this);
            tree.root = tree.root.Clone();
            return tree;
        }
    }
}
