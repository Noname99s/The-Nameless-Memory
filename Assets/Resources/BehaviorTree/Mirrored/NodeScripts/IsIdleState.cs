using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "IsIdleState",menuName = "BehaviorNodes/Decorator/IsIdleState")]
public class IsIdleState : Decorator
{
   public MirroredBlackboard blackboard;
   public override bool Condition()
   {
        return true;
   }
}