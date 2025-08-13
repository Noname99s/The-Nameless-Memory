using Orpul1;
using Orpul1.UI;
using System.Collections;
using UnityEngine;

public class WillFragmentLogic : TrackingCameraBase
{
    [SerializeField] SO_DialogDataBase firstRespawnStatic;
    [SerializeField] SO_DialogDataBase firstRespawnDynamic;
    [SerializeField] SO_DialogDataBase fistFragmentCollected;
    void Start()
    {
        Init();
        firstRespawnStatic.Init();
        firstRespawnDynamic.Init();
        fistFragmentCollected.Init();
        if (Managers.Game.playData.dialogComplete[(int)Stage.Stage1].datas[firstRespawnStatic._dialogIndex] == false)
        {
            firstRespawnStatic.TransferDialogToUI();
            Managers.Game.playData.dialogComplete[(int)Stage.Stage1].datas[firstRespawnStatic._dialogIndex] = true;
            StartCoroutine(StartDynamic());
            Managers.Game.playData.dialogComplete[(int)Stage.Stage1].datas[firstRespawnDynamic._dialogIndex] = true;
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
            Managers.Sound.PlayUISound((int)ESystemSound.MemoryPieceGain);
            Managers.Map.getFragment = true;
            Destroy(gameObject);
        }
    }
    public IEnumerator StartDynamic()
    {
        yield return new WaitForSeconds(0.3f);
        firstRespawnDynamic?.TransferDialogToUI();
        StartCoroutine(TrackingScene());
    }
    public override IEnumerator TrackingScene()
    {
        TrackingStart();
        yield return new WaitForSeconds(3f);
        TrackingEnd();
    }
    private void OnDestroy()
    {
        firstRespawnStatic.ClearAction();
        firstRespawnDynamic.ClearAction();
        fistFragmentCollected.ClearAction();
    }
}