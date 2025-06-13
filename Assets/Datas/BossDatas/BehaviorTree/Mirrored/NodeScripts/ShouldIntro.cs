using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "ShouldIntro",menuName = "BehaviorNodes/Decorator/ShouldIntro")]
public class ShouldIntro : Decorator
{
   public MirroredBlackboard blackboard;
   public override bool Condition()
   {
        return blackboard.btComponent.bossInfo.shouldIntro;
   }
}