using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

namespace SOBehaviorTree
{
    public class ActionNode : LeafNode, IActionable
    {
        [SerializeField] public ActionScript actionScript;

        bool isStart = false;
        public override void Init()
        {
            nodeState = NodeState.Stopped;
            if (actionScript == null)
            {
                Debug.LogError("Null Action!");
                return;
            }
            actionScript.Init();
            isStart = false;
        }
        public override NodeState Process()
        {
            if(!isStart) 
                OnStart();
            NodeState nodeState = OnUpdate();
            if (isStart && nodeState != NodeState.Running) 
                OnExit();
            return nodeState;
        }
        public NodeState OnStart()
        {
            nodeState = NodeState.Running;
            isStart = true;
            return actionScript.OnStart();
        }

        public NodeState OnUpdate()
        {
            nodeState = NodeState.Running;
            return actionScript.OnUpdate();
        }

        public NodeState OnExit()
        {
            nodeState = NodeState.Stopped;
            isStart = false;
            return actionScript.OnExit();
        }
        public override void AbortNode()
        {
            OnExit();
        }
    }
}