using UnityEngine;
using System.Collections.Generic;

public abstract class StageGimicBase : MonoBehaviour
{
    [SerializeField] protected Stage stage; // 스테이지 정보
    [SerializeField] protected int interactionCount = 0; // 스테이지 상호작용 횟수
    public event System.Action<string> OnStageStart; // 이벤트: 스테이지 시작
    void Awake()
    {
        Managers.Game.StartStage(stage, interactionCount);
        OnStageStart += Managers.UI.GetPopupUI<InGameComposition>().OnSystemTextUpdateConnection;
        NotifyStage();
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
    void OnDestroy()
    {
        OnStageStart -= Managers.UI.GetPopupUI<InGameComposition>().OnSystemTextUpdateConnection;
    }
}
