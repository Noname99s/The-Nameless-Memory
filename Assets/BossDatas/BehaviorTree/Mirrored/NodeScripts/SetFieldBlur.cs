using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Camera;
using Orpul1;

[CreateAssetMenu(fileName = "SetFieldBlur",menuName = "BehaviorNodes/Action/SetFieldBlur")]
public class SetFieldBlur : ActionScript
{
    public MirroredBlackboard blackboard;
    ICameraEffectAble setCameraEffect;
    public ECameraEffect effectType = ECameraEffect.None;
    public bool isActive = false;
    public override void Init()
    {
        SetCameraEffect();
    }
    public override NodeState OnStart()
    {
        SetCameraEffect();
        return NodeState.Success;
    }
    public override NodeState OnUpdate()
    {
        if(isActive)
            setCameraEffect.Execute((int)effectType);
        else
            setCameraEffect.Abort((int)effectType);
        return NodeState.Success;
    }
    public override NodeState OnExit()
    {
        return NodeState.Success;
    }
    private void SetCameraEffect()
    {
        if (setCameraEffect != null)
            return;
        setCameraEffect = CameraManager.Instance.GetCamera((int)ECameraType.MainCamera).GetComponent<ICameraEffectAble>();

    }
}
