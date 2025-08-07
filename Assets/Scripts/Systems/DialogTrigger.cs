using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] Stage _stage = Stage.Stage1; // Stage where the dialog is triggered
    [SerializeField] SO_DialogDataBase _dialogData = null;
    public bool _isDisplayed = false;
    void Start()
    {
        if(Managers.Game.playData.dialogComplete[(int)_stage].datas[_dialogData._dialogIndex])
            _isDisplayed = true;
        _dialogData.Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player") && !_isDisplayed)
        {
            _isDisplayed = true;
            Managers.Game.playData.dialogComplete[(int)_stage].datas[_dialogData._dialogIndex] = true; 
            _dialogData.TransferDialogToUI();
        }
    }
}
