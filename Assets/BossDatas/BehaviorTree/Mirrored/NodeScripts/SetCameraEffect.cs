using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Camera;
using Orpul1;

[CreateAssetMenu(fileName = "SetCameraEffect", menuName = "BehaviorNodes/Action/SetCameraEffect")]
public class SetCameraEffect : ActionScript
{
    public MirroredBlackboard blackboard;
    [SerializeField] ECameraEffect effectType;
    [SerializeField] bool isExecute = true;
    ICameraEffectAble setCameraEffect;
    public override void Init()
    {
        setCameraEffect = blackboard.btComponent.bossSkillInfo.cameraEffect;
    }
    public override NodeState OnStart()
    {
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        if (isExecute)
            setCameraEffect.Execute((int)effectType);
        else
            setCameraEffect.Stop((int)effectType);
            return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
}
