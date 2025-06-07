using Orpul1;
using System.Collections.Generic;
using UnityEngine;

public class HubDoor : MonoBehaviour, IInteractAble
{
    [SerializeField] Stage stageType = Stage.Stage1;
    [SerializeField] SO_MapInfo[] stageInfo;
    [SerializeField] bool isOpen = false;
    bool isInDoor = false;
    public event System.Action<List<string>> OnCannotEnter;
    void Start()
    {
        OnCannotEnter += Managers.UI.GetPopupUI<InGameComposition>().OnDialogAnimUpdateConnection;
        ConfirmClear();
    }
    private void OnDestroy()
    {
        OnCannotEnter -= Managers.UI.GetPopupUI<InGameComposition>().OnDialogAnimUpdateConnection;
    }

    void ConfirmClear()
    {
        if (stageType != Stage.Stage1 && Managers.Game.playData.stageCleared[(int)stageType - 1])
        {
            isOpen = true;
        }
        if(stageType == Stage.BossStage && Managers.Game.playData.stageCleared[(int)Stage.Stage2])
        {
            isOpen = true;
        }
    }

    public void Entrance()
    {
        if (!isOpen)
        {
            // Cannot enter Dialog
            List<string> cannotEnterText = new List<string>
            {
                "아직은 들어갈 수 없어. 앞선 죄목을 먼저 파해치자."
            };
            OnCannotEnter?.Invoke(cannotEnterText);
            return;
        }
        if(stageType == Stage.BossStage)
        {
            Managers.Game.ChangeState(GameState.Boss);
            return;
        }
        if (Managers.Map.stageType != stageType)
        {
            Managers.Map.stageType = stageType;
            Managers.Map.mapInfos.Clear(); 
            for (int i = 0; i < stageInfo.Length; i++)
            {
                Managers.Map.mapInfos.Add(stageInfo[i]);
            }
        }
        Managers.Game.ChangeState(GameState.Battle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInDoor = false;
        }
    }

    public void Interact()
    {
        Entrance();
    }
}
