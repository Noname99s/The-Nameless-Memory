using SOBehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public BehaviorNode node;
    public Port inputPort;
    public Port outputPort;

    // >> : 노드 정보 설정 및 디자인 경로 가져와 세팅
    public NodeView(BehaviorNode node) : base("Assets/Tools/BehaviorTreeEditor/Editor/UIDocuments/NodeView/NodeView.uxml") 
    {
        this.node = node;
        this.title = node.nodeName;
        Label description = this.Q<Label>("Description");
        description.text = node.description;
        this.viewDataKey = node.guid;
        
        style.left = node.position.x;
        style.top = node.position.y;
        

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();
    }
    // << :

    private void SetupClasses()
    {
        if(node is RootNode)
        {
            AddToClassList("RootNode");
        }
        else if (node is DecoratorNode)
        {
            AddToClassList("DecoNode");
        }
        else if (node is ActionNode)
        {
            AddToClassList("ActionNode");
        }
        else if (node is ControlFlowNode)
        {
            AddToClassList("ControlFlowNode");
        }
        
    }

    private void CreateInputPorts()
    {
        if (!(node is PanelNode)&&!(node is ActionNode) && !(node is ControlFlowNode) && !(node is LeafNode)) return;

        inputPort = InstantiatePort(Orientation.Vertical,
                                    UnityEditor.Experimental.GraphView.Direction.Input,
                                    Port.Capacity.Single,
                                    typeof(bool));

        if (inputPort == null) return;

        inputPort.portName = "";
        inputPort.style.flexDirection = FlexDirection.Column;
        inputPort.StretchToParentSize();
        inputContainer.Add(inputPort);
        
    }

    private void CreateOutputPorts()
    {
        if (node is ControlFlowNode)
        {
            outputPort = InstantiatePort(Orientation.Vertical,
                                UnityEditor.Experimental.GraphView.Direction.Output,
                                Port.Capacity.Multi,
                                typeof(bool));
        }
        else if (node is DecoratorNode || node is RootNode) 
        {
            outputPort = InstantiatePort(Orientation.Vertical,
                                UnityEditor.Experimental.GraphView.Direction.Output,
                                Port.Capacity.Single,
                                typeof(bool));
        }

        if (outputPort == null) return;

        outputPort.portName = "";
        outputPort.style.flexDirection = FlexDirection.ColumnReverse;
        outputContainer.Add(outputPort);
    }

 
    //에디터 종료 전 이전 위치 저장
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
    }
    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelected != null)
        {
            OnNodeSelected.Invoke(this);
        }
    }

    public void UpdateState()
    {
        RemoveFromClassList("Success");
        RemoveFromClassList("Failure");
        RemoveFromClassList("Running");
        //if (!Application.isPlaying) return;
        switch(node.nodeState)
        {
            case NodeState.Success:
                AddToClassList("Success");
                break;
            case NodeState.Failure:
                AddToClassList("Failure");
                break;
            case NodeState.Running:
                if(node.nodeState != NodeState.Stopped)
                    AddToClassList("Running");
                break;
        }
        node.nodeState = NodeState.Stopped;
    }
}
