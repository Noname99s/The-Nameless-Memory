using UnityEngine;

public class DynamicDialogTrigger : MonoBehaviour
{
    [SerializeField] Stage _stage = Stage.Stage1; // Stage where the dialog is triggered    
    [SerializeField] SO_DynamicDialogData _dynamicDialogData = null;
    bool _isDisplayed = false;

    void Start()
    {
        _isDisplayed = Managers.Game.playData.dialogComplete[(int)_stage].datas[_dynamicDialogData._dialogIndex];
        _dynamicDialogData.Init();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isDisplayed)
        {
            _isDisplayed = true;
            Managers.Game.playData.dialogComplete[(int)_stage].datas[_dynamicDialogData._dialogIndex] = true;
            _dynamicDialogData.TransferDialogToUI();
        }
    }
}
