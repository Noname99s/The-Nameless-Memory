using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "IsPhase1",menuName = "BehaviorNodes/Decorator/IsPhase1")]
public class IsPhase1 : Decorator
{
   public MirroredBlackboard blackboard;
   public override bool Condition()
   {
        if (blackboard.btComponent.hp <= blackboard.btComponent.phaseHpConditions[(int)EPhaseType.Phase1]) return false;
        return true;
   }
}