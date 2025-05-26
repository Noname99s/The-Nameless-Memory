using System.Collections;
using System.Collections.Generic;
using SOBehaviorTree;
using UnityEngine;

public abstract class ActionScript : BehaviorNode, IActionable
{
    public override void Init()
    {
    }
    public virtual NodeState OnStart()
    {
        return NodeState.Success;
    }

    public virtual NodeState OnUpdate()
    {
        return NodeState.Success;
    }

    public virtual NodeState OnExit()
    {
        return NodeState.Success;
    }
}
