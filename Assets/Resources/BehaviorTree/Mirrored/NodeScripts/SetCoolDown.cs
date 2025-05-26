using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
[CreateAssetMenu(fileName = "SetCoolDown",menuName = "BehaviorNodes/Action/SetCoolDown")]
public class SetCoolDown : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] EBossSkill bossSkill;
    [SerializeField] float coolTime = 0;
    public override NodeState OnStart()
    {
        blackboard.btComponent.leftSkillCoolTimes[(int)bossSkill] = Time.time + coolTime;
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
