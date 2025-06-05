using Orpul1;
using UnityEngine;

public class HubDoor : MonoBehaviour, IInteractAble
{
    [SerializeField] Stage stageType = Stage.Stage1;
    [SerializeField] SO_MapInfo[] stageInfo;
    [SerializeField] bool isOpen = false;
    bool isInDoor = false;
    public event System.Action<string> OnCannotEnter;
    void Start()
    {
        OnCannotEnter += Managers.UI.GetPopupUI<InGameComposition>().OnDialogAnimUpdateConnection;
        ConfirmClear();
    }
    /*void Update()
    {
        if (isInDoor && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Entrance();
        }
    }*/
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
            OnCannotEnter?.Invoke("아직은 들어갈 수 없어. 앞선 죄목을 먼저 파해치자.");
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
