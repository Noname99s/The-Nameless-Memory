using System.Collections;
using System.Collections.Generic;
using SOBehaviorTree;
using UnityEngine;

public abstract class ActionScript : BehaviorNode, IActionable
{
    public virtual NodeState OnStart()
    {
        throw new System.NotImplementedException();
    }

    public virtual NodeState OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public virtual NodeState OnExit()
    {
        throw new System.NotImplementedException();
    }
}
