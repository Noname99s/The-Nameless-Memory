using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "IsPhase1",menuName = "BehaviorNodes/Decorator/IsPhase1")]
public class IsPhase : Decorator
{
    public MirroredBlackboard blackboard;
    [SerializeField] EPhaseType phaseType;
   public override bool Condition()
   {
        if(!IsInPhase()) return false;
        return true;
   }
    private bool IsInPhase()
    {
        if (blackboard.btComponent.hp > blackboard.btComponent.phaseHpConditions[(int)phaseType].range1) return false;
        if (blackboard.btComponent.hp <= blackboard.btComponent.phaseHpConditions[(int)phaseType].range2) return false;
        return true;
    }
}