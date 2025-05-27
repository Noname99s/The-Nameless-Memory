using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using SOBehaviorTree;
using System;
using Codice.Client.BaseCommands.BranchExplorer;

public class BehaviorTreeView : GraphView
{
    public Action<NodeView> OnNodeSelected;
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> { }
    SOBehaviorTree.BehaviorTree tree;
    TemplateScript templateScript = new TemplateScript();
    BlackboardView blackBoardView;
    Type[] panelNodes = new Type[]
    {
        new PanelNode().GetType(),
    };
    Type[] customClassMenus = new Type[]
    {
        new ActionNode().GetType(),
        new DecoratorNode().GetType(),
    };
    Type[] leafNodeMenus = new Type[]
    {
        new ActionNode().GetType(),
        new DecoratorNode().GetType(),
        new DelayNode().GetType(),
        new ReturnFailerNode().GetType(),
        new ReturnSuccessNode().GetType(),
    };
    Type[] controlFlowMenus = new Type[]
    {
        
        new SequenceNode().GetType(),
        new SelectorNode().GetType(),
        new ParallelNode().GetType(),
        new RandomSelectorNode().GetType(),
        
    };
           

    public BehaviorTreeView()
    {
        Insert(0, new GridBackground());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        var styleSheet = (StyleSheet)EditorGUIUtility.Load("Assets/Tools/BehaviorTreeEditor/Editor/UIDocuments/BehaviorTreeEditor/BehaviorTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }

    NodeView FindNodeView(BehaviorNode node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }
    internal void PlayModePopulateview(SOBehaviorTree.BehaviorTree tree)
    {
        InitView(tree);
        CreateRootNode();
        CreateNodes();
        CreateEdges();
    }
    internal void EditorModePopulateview(SOBehaviorTree.BehaviorTree tree)
    {
        InitView(tree);
        SetBlackboard();
        CreateRootNode();
        CreateNodes();
        CreateEdges();
        templateScript.SetBlackboardName(tree.blackboardName);
        templateScript.CreateTemplateNode(CreateNode, tree.blackboard);
        
    }
    void InitView(SOBehaviorTree.BehaviorTree tree)
    {
        this.tree = tree;
        this.tree.Initialize();

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;
    }
    void SetBlackboard()
    {
        blackBoardView = new BlackboardView(tree);
        blackBoardView.SetBlackboard();
        templateScript.SetTreePath(tree.TreePath);
    }
    void CreateRootNode()
    {
        //Create Root Node View
        if (tree.root != null) return;

        tree.root = tree.CreateNode(typeof(RootNode), "RootNode") as RootNode;
        EditorUtility.SetDirty(tree);
        AssetDatabase.SaveAssets();
    }
    void CreateNodes()
    {
        // Create Node View
        tree.nodeList.ForEach(n => CreateNodeView(n));
    }
    void CreateEdges()
    {
        // Create Edge(이전에 연결한 노드 다시 연결)
        tree.nodeList.ForEach(n =>
        {
            var childeren = tree.GetChilds(n);
            childeren.ForEach(c =>
            {
                NodeView parentView = FindNodeView(n);
                NodeView childView = FindNodeView(c);
                Edge edge = parentView.outputPort.ConnectTo(childView.inputPort);
                AddElement(edge);
            });
        });
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> retPortList = new List<Port>();
        List<Port> portList = ports.ToList();
        for(int i=0; i < portList.Count; i++)
        {
            if (portList[i].direction!= startPort.direction&&
                portList[i].node != startPort.node)
            {
                retPortList.Add(portList[i]);
            }
        }
        
        return retPortList;
    }
    
    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
   {
        RemoveElements(graphViewChange);
        graphViewChange = ConnectChildNodes(graphViewChange);

        return graphViewChange;
    }
    void RemoveElements(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove == null) return;
        List<GraphElement> removeElements = graphViewChange.elementsToRemove;
        RemoveNodes(removeElements);
        RemoveEdges(removeElements);
    }
    GraphViewChange ConnectChildNodes(GraphViewChange graphViewChange)
    {
        //현재 연결된 자식 노드 추가
        if (graphViewChange.edgesToCreate == null) return graphViewChange;

        graphViewChange.edgesToCreate.ForEach(edge =>
        {
            NodeView parentView = edge.output.node as NodeView;
            NodeView childView = edge.input.node as NodeView;
            tree.AddChilde(parentView.node, childView.node);
        });
        return graphViewChange; 
    }
    void RemoveNodes(List<GraphElement> removeElements)
    {
        for (int i = 0; i < removeElements.Count; i++)
        {
            NodeView nodeView = removeElements[i] as NodeView;
            if (nodeView == null) return;

            tree.DeleteNode(nodeView.node);
        }
    }
    void RemoveEdges(List<GraphElement> removeElements)
    {
        for (int i = 0; i < removeElements.Count; i++)
        {
            Edge edge = removeElements[i] as Edge;
            if (edge == null) continue;

            NodeView parentView = edge.output.node as NodeView;
            NodeView childView = edge.input.node as NodeView;
            tree.RemoveChilde(parentView.node, childView.node);
        }
    }
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        foreach (Type menu in panelNodes)
        {
            evt.menu.AppendAction($"{"PanelNode"}", (a) => CreateNode(menu, menu.Name));
        }
        foreach (Type menu in customClassMenus)
        {
            evt.menu.AppendAction($"{"CustomNodes/"}[{menu.BaseType.Name}]{menu.Name}", (a) => SetTemplateClass(menu));
        }
        foreach (Type menu in controlFlowMenus)
        {
            evt.menu.AppendAction($"{"Nodes/ControlFlow/"}[{menu.BaseType.Name}]{menu.Name}", (a) => CreateNode(menu, menu.Name));
        }
        foreach (Type menu in leafNodeMenus)
        {
            evt.menu.AppendAction($"{"Nodes/LeafNode/"}[{menu.BaseType.Name}]{menu.Name}", (a) => CreateNode(menu, menu.Name));
        }
    }
    
    void SetTemplateClass(Type type)
    {
        new BTCreateWindow(type, CreateTemplateClass);
    }
    void CreateTemplateClass(Type classType, string nodeName)
    {
        templateScript.CreateClass(classType, nodeName);
    }
    void CreateNode(Type type, string nodeName)
    {
        BehaviorNode node = tree.CreateNode(type, nodeName);
        CreateNodeView(node);
    }
    void CreateNode(Type nodeType,object nodeClass, string nodeName)
    {
#if UNITY_EDITOR
        BehaviorNode node = tree.CreateNode(nodeType, nodeClass, nodeName);
        CreateNodeView(node);
#endif
    }

    void CreateNodeView(BehaviorNode node)
    {
        node.SetEditorNodeName();
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    public void UpdateNodeState()
    {
        List<Node> nodeList = nodes.ToList();
        for (int i = 0; i < nodeList.Count; i++)
        {
            NodeView nodeView = nodeList[i] as NodeView;
            if (nodeView == null) continue;

            nodeView.UpdateState();
        }
    }
}
