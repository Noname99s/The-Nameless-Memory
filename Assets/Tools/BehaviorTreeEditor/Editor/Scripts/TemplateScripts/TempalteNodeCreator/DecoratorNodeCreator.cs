using System;
using System.Text;
using SOBehaviorTree;
using UnityEditor;

public class DecoratorNodeCreator : TemplateScripteCreator
{
    string scriptPath = "Assets/Tools/BehaviorTreeEditor/Editor/TemplateScripts/TemplateDecoratorNode.txt";
    string menuPath = "BehaviorNodes/Decorator/";
    public override void CreateClass(StringBuilder className, StringBuilder blackboardName, string treePath)
    {
        base.CreateClass(className, blackboardName, treePath);
        EditorPrefs.SetInt(NodeKeyType.NodeType.ToString(), (int)TemplateScript.ENodeType.DecoratorNode);
        WriteFile(scriptPath, menuPath, treePath);
    }
}