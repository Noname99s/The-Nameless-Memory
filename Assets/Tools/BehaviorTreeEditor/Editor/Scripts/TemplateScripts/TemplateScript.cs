using System;
using System.Text;
using SOBehaviorTree;
using UnityEngine;

public class TemplateScript
{
    public enum ENodeType
    { 
        ActionNode,
        DecoratorNode
    }
    TemplateNodeCreator templateNodeCreator = new TemplateNodeCreator();

    TemplateScripteCreator[] templateScripteCreators = new TemplateScripteCreator[2]
    {
        new ActionNodeCreator(),
        new DecoratorNodeCreator()
    };
    string blackboardName;
    string treePath = "";
    public void SetTreePath(string path)
    {
        this.treePath = path;
    }
    public void CreateTemplateNode(Action<Type, object, string> createNode, ScriptableObject blackboard)
    {
        templateNodeCreator.CreateTemplateNode(createNode, blackboard);
    }
    public void SetBlackboardName(string name)
    {
        blackboardName = name;
    }
    public void CreateClass(Type type, string nodeName)
    {
        templateScripteCreators[GetIndex(type)].CreateClass(new StringBuilder(nodeName), new StringBuilder(blackboardName), treePath);
    }

    private int GetIndex(Type type)
    {
        if(type.Name == nameof(ActionNode))
            return (int)ENodeType.ActionNode;
        else
            return (int)ENodeType.DecoratorNode;
    }

}