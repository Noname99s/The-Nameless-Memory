using UnityEngine;
using System.Collections.Generic;

public abstract class StageGimicBase : MonoBehaviour
{
    [SerializeField] protected Stage stage; // 스테이지 정보
    [SerializeField] protected int interactionCount = 0; // 스테이지 상호작용 횟수

    void Awake()
    {
        Managers.Game.StartStage(stage, interactionCount);
    }
}
