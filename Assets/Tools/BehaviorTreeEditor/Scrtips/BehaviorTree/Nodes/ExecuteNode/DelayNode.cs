using System.Collections;
using System.Collections.Generic;
using SOBehaviorTree;
using UnityEngine;

public class DelayNode : LeafNode
{
    [SerializeField] float delayTime;
    [SerializeField] float interval = 0;
    public override void Init()
    {
        interval = 0;
    }
    public override NodeState Process()
    {
        nodeState = NodeState.Running;
        interval += Time.deltaTime;
        if(interval < delayTime) return NodeState.Running;
        nodeState = NodeState.Stopped;
        interval = 0;
        return NodeState.Success;
    }
}
