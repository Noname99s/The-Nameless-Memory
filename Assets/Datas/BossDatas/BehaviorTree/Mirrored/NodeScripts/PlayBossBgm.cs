using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss;
using DG.Tweening;
using Orpul1.Sound;

[CreateAssetMenu(fileName = "PlayBossBgm",menuName = "BehaviorNodes/Action/PlayBossBgm")]
public class PlayBossBgm : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] EBossBGM bossBGM;
    public override void Init()
    {
        blackboard.btComponent.bossInfo.sliderUI.SetActive(false);
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        Managers.Sound.PlaySound2D(ESoundCategory.BOSS, (int)bossBGM, EMixerGroup.BossEffect, true);
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
