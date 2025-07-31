using UnityEngine;
using System.Collections.Generic;

public abstract class StageGimicBase : MonoBehaviour , IStageGimic
{
    [SerializeField] protected Stage stage; // 스테이지 정보
    [SerializeField] protected int interactionCount = 0; // 스테이지 상호작용 횟수
    public event System.Action<string> OnStageStart; // 이벤트: 스테이지 시작

    int currentMapIndex = 0; // 현재 맵 인덱스
    [SerializeField] List<GameObject> stageObjects; // Stage 2 specific objects
    void Awake()
    {
        Managers.Map.currentStage = this; // 현재 스테이지 설정
        Managers.Game.StartStage(stage, interactionCount);
        OnStageStart += Managers.UI.GetPopupUI<InGameComposition>().OnSystemTextUpdateConnection;
    }
    protected virtual void Start()
    {
        NotifyStage();
    }
    public void TransitionMap(int index)
    {
        stageObjects[currentMapIndex].SetActive(false); // 현재 맵 오브젝트 비활성화
        currentMapIndex = index; // 새로운 맵 인덱스 설정
        if (currentMapIndex < 0 || currentMapIndex >= stageObjects.Count)
        {
            Debug.LogError("Invalid map index: " + currentMapIndex);
            return;
        }
        stageObjects[currentMapIndex].SetActive(true); // 새로운 맵 오브젝트 활성화
    }
    void NotifyStage()
    {
        switch (stage)
        {
            case Stage.Stage1:
                OnStageStart?.Invoke(BG_DB_Dialogtext.GetEntity("Text_UI")._DialogLines[0]._Text);
                break;
            case Stage.Stage2:
                OnStageStart?.Invoke(BG_DB_Dialogtext.GetEntity("Text_UI")._DialogLines[1]._Text);
                break;
        }
    }
}
