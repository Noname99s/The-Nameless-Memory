using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "BossOutro",menuName = "BehaviorNodes/Action/BossOutro")]
public class BossOutro : ActionScript
{
    public MirroredBlackboard blackboard;

    public override void Init()
    {
        
    }
    public override NodeState OnStart()
    {
        GameObject bossObj = blackboard.btComponent.bossInfo.bossObj;
        bossObj.GetComponent<BossController>().BossOutro
            (
                () =>
                {
                    bossObj.SetActive(false);
                    Managers.Game.ChangeState(GameState.Ending);
                }
            );
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        return NodeState.Running;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
