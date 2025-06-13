using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Sound;
using Mono.Cecil.Cil;

[CreateAssetMenu(fileName = "PlayBossSound",menuName = "BehaviorNodes/Action/PlayBossSound")]
public class PlayBossSound : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] EBossSound soundType;
    public override void Init()
    {
        
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        blackboard.btComponent.sound.PlaySound((int)soundType);
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
