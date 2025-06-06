using UnityEngine;

public class Stage1Gimic : StageGimicBase
{
    [SerializeField] SO_StaticDialogData firstStart;
    private void Start()
    {
        firstStart.Init();
        if (!Managers.Game.playData.dialogComlete[(int)stage][0])
        {
            firstStart.TransferDialogToUI();
            Managers.Game.playData.dialogComlete[(int)stage][0] = true;
        }
    }
}
