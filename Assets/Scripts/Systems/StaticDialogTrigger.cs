using UnityEngine;

public class StaticDialogTrigger : MonoBehaviour
{
    [SerializeField] Stage _stage = Stage.Stage1; // Stage where the dialog is triggered
    [SerializeField] SO_StaticDialogData _staticDialogData = null;
    public bool _isDisplayed = false;
    void Start()
    {
        if(Managers.Game.playData.dialogComplete[(int)_stage].datas[_staticDialogData._dialogIndex])
            _isDisplayed = true;
        _staticDialogData.Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player") && !_isDisplayed)
        {
            _isDisplayed = true;
            Managers.Game.playData.dialogComplete[(int)_stage].datas[_staticDialogData._dialogIndex] = true; 
            _staticDialogData.TransferDialogToUI();
        }
    }
}
