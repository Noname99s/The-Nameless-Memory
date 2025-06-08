using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;

[CreateAssetMenu(fileName = "BossIntroEnd",menuName = "BehaviorNodes/Action/BossIntroEnd")]
public class BossIntroEnd : ActionScript
{
    public MirroredBlackboard blackboard;
    Transform endPosi;
    public override void Init()
    {
        blackboard.btComponent.bossInfo.sliderUI.SetActive(false);
        endPosi = blackboard.btComponent.bossInfo.sliderMovePosis[1];
    }
    public override NodeState OnStart()
    {
        blackboard.btComponent.bossInfo.sliderUI.SetActive(true);
        blackboard.btComponent.bossInfo.sliderUI.transform.DOMove(endPosi.position, 1);
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        blackboard.btComponent.bossInfo.shouldIntro = false;
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
