using UnityEngine;

namespace SOBehaviorTree
{
    public class RepeatNode : ControlFlowNode
    {
        [SerializeField] int repeatCnt = 1;
        public override NodeState Process()
        {
            nodeState = childNodeList[0].Process();

            if (IsFailure())
                return NodeState.Failure;

            if (IsOutofRepeatCnt())
                return NodeState.Success;

            if(IsSuccess())
                curIndex++;

            return NodeState.Running;
        }
        private bool IsOutofRepeatCnt()
        {
            if (curIndex < repeatCnt-1) return false;
            curIndex = 0;
            return true;
        }
        private bool IsSuccess()
        {
            if (nodeState != NodeState.Success)
                return false;

            return true;
        }

        private bool IsFailure()
        {
            if (nodeState != NodeState.Failure) 
                return false;

            curIndex = 0;
            return true;
        }

        public override void AbortNode()
        {
            childNodeList[0].AbortNode();
            curIndex = 0;
        }
    }
}
