using UnityEngine;

public class HubGimic : StageGimicBase
{
    [SerializeField] SO_StaticDialogData firstStart;
    [SerializeField] SO_DynamicDialogData firstStartDynamic;
    [SerializeField] SO_StaticDialogData clearStage2Notify;
    [SerializeField] DialogTrigger clearStage2;
    [SerializeField] DynamicDialogTrigger clearStage2Dynamic;
    protected override void Start()
    {
        base.Start();
        firstStart.Init();
        firstStartDynamic.Init();
        clearStage2Notify.Init();
        if (!Managers.Game.playData.dialogComplete[(int)stage].datas[0])
        {
            firstStart.TransferDialogToUI();
            Managers.Game.playData.dialogComplete[(int)stage].datas[0] = true;
        }
        if (!Managers.Game.playData.dialogComplete[(int)stage].datas[1])
        {
            firstStartDynamic.TransferDialogToUI();
            Managers.Game.playData.dialogComplete[(int)stage].datas[1] = true;
        }
        if (!Managers.Game.playData.stageCleared[(int)Stage.Stage2])
        {
            clearStage2._isDisplayed = true;
            clearStage2Dynamic._isDisplayed = true;
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
