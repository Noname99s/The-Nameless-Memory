using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;

[CreateAssetMenu(fileName = "IsSkill1CoolReady",menuName = "BehaviorNodes/Decorator/IsSkill1CoolReady")]
public class IsSkillCoolReady : Decorator
{
   public MirroredBlackboard blackboard;
    public EBossSkill bossSkill;
   public override bool Condition()
   {
        if (Time.time < blackboard.btComponent.bossSkillInfo.leftSkillCoolTimes[(int)bossSkill]) return false;
        return true;
   }
}