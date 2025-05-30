using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "IsDie",menuName = "BehaviorNodes/Decorator/IsDie")]
public class IsDie : Decorator
{
   public MirroredBlackboard blackboard;
   public override bool Condition()
   {
        return blackboard.btComponent.bossInfo.hp <= 0;
   }
}