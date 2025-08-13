using UnityEngine;

public class HubGimic : StageGimicBase
{
    [SerializeField] SO_DialogDataBase firstStart;
    [SerializeField] SO_DialogDataBase firstStartDynamic;
    [SerializeField] SO_DialogDataBase clearStage2Notify;
    [SerializeField] DialogTrigger clearStage2;
    protected override void Start()
    {
        base.Start();
        firstStart.Init();
        firstStartDynamic.Init();
        clearStage2Notify.Init();
        if (!Managers.Game.playData.dialogComplete[(int)stage].datas[firstStart._dialogIndex])
        {
            firstStart.TransferDialogToUI();
            Managers.Game.playData.dialogComplete[(int)stage].datas[firstStart._dialogIndex] = true;
        }
        if (!Managers.Game.playData.dialogComplete[(int)stage].datas[firstStartDynamic._dialogIndex])
        {
            firstStartDynamic.TransferDialogToUI();
            Managers.Game.playData.dialogComplete[(int)stage].datas[firstStartDynamic._dialogIndex] = true;
        }
        if (!Managers.Game.playData.stageCleared[(int)Stage.Stage2])
        {
            clearStage2._isDisplayed = true;
        }
        else
        {
            clearStage2Notify.TransferDialogToUI();
            Managers.Game.playData.dialogComplete[(int)stage].datas[clearStage2Notify._dialogIndex] = true;
        }
    }
    private void OnDestroy()
    {
        firstStart.ClearAction();
        firstStartDynamic.ClearAction();
        clearStage2Notify.ClearAction();
    }

}
