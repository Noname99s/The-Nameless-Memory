using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "Skill1CoolTimeReady",menuName = "BehaviorNodes/Decorator/Skill1CoolTimeReady")]
public class Skill1CoolTimeReady : Decorator
{
   public MirroredBlackboard blackboard;
   public override bool Condition()
   {
        return true;
   }
}