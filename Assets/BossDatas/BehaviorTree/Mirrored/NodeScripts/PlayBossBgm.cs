using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss;

[CreateAssetMenu(fileName = "PlayBossBgm",menuName = "BehaviorNodes/Action/PlayBossBgm")]
public class PlayBossBgm : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] EBossBGM bossBGM;

    public override void Init()
    {
        
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        Managers.Sound.PlayBGM(blackboard.btComponent.bossInfo.bossBGM[(int)bossBGM]);
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
