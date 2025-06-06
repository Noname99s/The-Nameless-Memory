using UnityEngine;
using System.Collections.Generic;

public class StageInteractions : MonoBehaviour
{
    [SerializeField] Stage stage; // 스테이지 정보
    [SerializeField] int interactionCount = 0; // 스테이지 상호작용 횟수

    void Awake()
    {
        Managers.Map.StartStage(stage, interactionCount);
    }
}
