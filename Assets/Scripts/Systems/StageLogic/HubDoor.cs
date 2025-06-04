using UnityEngine;

public class HubDoor : MonoBehaviour
{
    [SerializeField] Stage stageType = Stage.Stage1;
    [SerializeField] SO_MapInfo[] stageInfo;
    [SerializeField] bool isOpen = false;
    bool isInDoor = false;
    void Start()
    {
        ConfirmClear();
    }
    void Update()
    {
        if (isInDoor && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Entrance();
        }
    }
    void ConfirmClear()
    {
        if (stageType != Stage.Stage1 && Managers.Game.stageCleared[(int)stageType - 1])
        {
            isOpen = true;
        }
        if(stageType == Stage.BossStage && Managers.Game.stageCleared[(int)Stage.Stage2])
        {
            isOpen = true;
        }
    }

    public void Entrance()
    {
        if (!isOpen)
        {
            // Cannot enter Dialog
            Debug.Log("Cannot enter this stage yet.");
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
        Managers.Map.movePoint = Managers.Map.mapInfos[0].entrancePoint;
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
}
