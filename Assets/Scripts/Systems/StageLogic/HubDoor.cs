using UnityEngine;

public class HubDoor : MonoBehaviour
{
    [SerializeField] SO_StageInfo stageInfo;
    [SerializeField] bool isOpen = false;
    bool isInDoor = false;
    void Start()
    {
        ConfirmClear();
        if (stageInfo.stageIndex != Stage.Stage1 && Managers.Game.stageCleared[(int)stageInfo.stageIndex - 1])
        {
            isOpen = true;
        }
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
        gameObject.SetActive(!Managers.Game.stageCleared[(int)stageInfo.stageIndex]);
    }

    public void Entrance()
    {
        if (!isOpen)
        {
            // Cannot enter Dialog
            Debug.Log("Cannot enter this stage yet.");
            return;
        }
        if (Managers.Map.mapListType != stageInfo.stageIndex)
        {
            Managers.Map.playerSpawnPoint = stageInfo.spawnPoint;
            Managers.Map.mapListType = stageInfo.stageIndex;
            Managers.Map.mapObjects.Clear();
            for (int i = 0; i < stageInfo.stageObjects.Length; i++)
            {
                Managers.Map.mapObjects.Add(stageInfo.stageObjects[i]);
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
}
