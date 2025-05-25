using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
[CreateAssetMenu(fileName = "IsPhase2",menuName = "BehaviorNodes/Decorator/IsPhase2")]
public class IsPhase2 : Decorator
{
   public MirroredBlackboard blackboard;
   public override bool Condition()
   {
        if (blackboard.btComponent.hp <= blackboard.btComponent.phaseHpConditions[(int)EPhaseType.Phase2]) return false;
        return true;
   }
}