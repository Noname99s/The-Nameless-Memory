using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[CreateAssetMenu(fileName = "BossDie",menuName = "BehaviorNodes/Action/BossDie")]
public class BossDie : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] Color dieStateColor;
    [SerializeField] float dissapearSpeed;
    private GameObject bossObj;
    private SpriteRenderer bossSprite;
    
    public override NodeState OnStart()
    {
        bossSprite = blackboard.btComponent.bossInfo.bossSprite;
        bossObj = blackboard.btComponent.bossInfo.bossObj;
        dieStateColor = bossSprite.color;
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        if (bossSprite.color.a <= 0) return NodeState.Success;
        dieStateColor.a -= dissapearSpeed * Time.deltaTime;
        bossSprite.color = dieStateColor;
        return NodeState.Running;
    }
    public override NodeState OnExit()
    {
        bossObj.SetActive(false);
        return NodeState.Success;
    }
}
