using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
[CreateAssetMenu(fileName = "IsIdleState",menuName = "BehaviorNodes/Decorator/IsIdleState")]
public class IsIdleState : Decorator
{
   public MirroredBlackboard blackboard;
   public override bool Condition()
    {
        if (blackboard.btComponent.bossInfo.bossState != EBossState.Idle) return false;
        return true;
   }
}