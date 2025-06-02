using UnityEngine;

public class HubDoor : MonoBehaviour
{
    [SerializeField] Stage stageIndex;
    [SerializeField] bool isOpen = false;
    [SerializeField] GameObject[] stages;
    void Start()
    {
        if (stageIndex != Stage.Stage1 && Managers.Game.stageCleared[(int)stageIndex - 1])
        {
            isOpen = true;
        }
    }

    public void ConfirmClear()
    {
        gameObject.SetActive(!Managers.Game.stageCleared[(int)stageIndex]);
    }

    public void Entrance()
    {
        if(!isOpen)
        {
            // Cannot enter Dialog
            Debug.Log("Cannot enter this stage yet.");
            return;
        }
        if(Managers.Map.mapListType == stageIndex)
        {
            // Already Apply map logic
            return;
        }
        Managers.Map.mapListType = stageIndex;
        Managers.Map.mapObjects.Clear();
        for(int i = 0; i < stages.Length; i++)
        {
            Managers.Map.mapObjects.Add(stages[i]);
        }
        Managers.Game.ChangeState(GameState.Battle);
    }

    // юс╫ц -----------------------------
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Entrance();
        }
    }
}
