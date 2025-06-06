using UnityEngine;

public class HubGimic : StageGimicBase
{
    [SerializeField] SO_StaticDialogData firstStart;
    [SerializeField] SO_DynamicDialogData firstStartDynamic;
    void Start()
    {
        firstStart.Init();
        firstStartDynamic.Init();
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
    }
   
}
