using System;
using System.Text;
using SOBehaviorTree;
using UnityEditor;

public class ActionNodeCreator: TemplateScripteCreator
{
    string scriptPath = "Assets/Tools/BehaviorTreeEditor/Editor/TemplateScripts/TemplateActionNode.txt";
    string menuPath = "BehaviorNodes/Action/";
    public override void CreateClass(StringBuilder className, StringBuilder blackboardName, string treePath)
    {
        base.CreateClass(className, blackboardName, treePath);
        EditorPrefs.SetInt(NodeKeyType.NodeType.ToString(), (int)TemplateScript.ENodeType.ActionNode);
        WriteFile(scriptPath, menuPath, treePath);
    }

}