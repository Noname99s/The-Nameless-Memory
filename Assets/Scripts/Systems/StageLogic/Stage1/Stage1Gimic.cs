using UnityEngine;

public class Stage1Gimic : StageGimicBase
{
    [SerializeField] SO_StaticDialogData firstStart;
    private void Start()
    {
        firstStart.Init();
        if (!Managers.Game.playData.dialogComplete[(int)stage].datas[0])
        {
            firstStart.TransferDialogToUI();
            Managers.Game.playData.dialogComplete[(int)stage].datas[0] = true;
        }
    }
}
