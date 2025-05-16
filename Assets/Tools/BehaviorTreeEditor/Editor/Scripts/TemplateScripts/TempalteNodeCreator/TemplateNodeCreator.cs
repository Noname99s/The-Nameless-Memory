using SOBehaviorTree;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class TemplateNodeCreator
{
    public Action<Type, string> createNode;
    private Type[] nodeTypes = new Type[2]
    {
        new ActionNode().GetType(),
        new DecoratorNode().GetType(),
    };

    IClassFactory classFactory = new ClassFactory();
    public void CreateTemplateNode(Action<Type, object, string> createNode,ScriptableObject blackboard)
    {
        if (!EditorPrefs.GetBool(NodeKeyType.NodeGuid.ToString())) return;
        EditorPrefs.SetBool(NodeKeyType.NodeGuid.ToString(), false);
        string className = EditorPrefs.GetString(NodeKeyType.NodeClassNameKey.ToString());

        object nodeObj= classFactory.Create(
                        className,
                        NodeKeyType.NodeScriptPathKey.ToString(),
                        NodeKeyType.NodeAssetPathKey.ToString()
                        );
        Type scriptType = GetNodeType(EditorPrefs.GetInt(NodeKeyType.NodeType.ToString(), 0));
        EditorPrefs.DeleteKey(NodeKeyType.NodeType.ToString());
        SetBlackboard(nodeObj, blackboard);
        createNode(scriptType, nodeObj, className);
    }
    private void SetBlackboard(object nodeObj, ScriptableObject blackboard)
    {
        if (!blackboard) return;
        ActionScript actionScript = nodeObj as ActionScript;
        FieldInfo blackboardField = nodeObj.GetType().GetField("blackboard");
        blackboardField.SetValue(nodeObj, blackboard);
    }
    private Type GetNodeType(int type)
    {
        return nodeTypes[type];
    }
}