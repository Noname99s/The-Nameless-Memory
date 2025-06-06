using Orpul1;
using UnityEngine;

public class WillFragmentLogic : MonoBehaviour
{
    [SerializeField] SO_StaticDialogData firstRespawnPlayer;
    [SerializeField] SO_DynamicDialogData fistFragmentCollected;
    void Start()
    {
        firstRespawnPlayer.Init();
        fistFragmentCollected.Init();
        if (Managers.Game.playData.dialogComplete[(int)Stage.Stage1].datas[firstRespawnPlayer._dialogIndex] == false)
        {
            firstRespawnPlayer.TransferDialogToUI();
            Managers.Game.playData.dialogComplete[(int)Stage.Stage1].datas[firstRespawnPlayer._dialogIndex] = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Managers.Game.playData.dialogComplete[(int)Stage.Stage1].datas[fistFragmentCollected._dialogIndex] == false)
            {
                fistFragmentCollected.TransferDialogToUI();
                Managers.Game.playData.dialogComplete[(int)Stage.Stage1].datas[fistFragmentCollected._dialogIndex] = true;
                other.GetComponent<ISkillOpenAble>()?.OpenSkill((int)EPlayerSkillType.DoubleJump);
            }
            Managers.Map.getFragment = true;
            Destroy(gameObject);
        }
    }
    
}
