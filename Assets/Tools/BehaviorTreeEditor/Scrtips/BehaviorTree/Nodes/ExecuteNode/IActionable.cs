using System.Collections;
using System.Collections.Generic;
using SOBehaviorTree;
using UnityEngine;
namespace SOBehaviorTree
{
    public interface IActionable
    {
        public NodeState OnStart();
        public NodeState OnUpdate();
        public NodeState OnExit();
    }
}
