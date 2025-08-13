using System.Collections;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] Stage _stage = Stage.Stage1; // Stage where the dialog is triggered
    [SerializeField] SO_DialogDataBase _staticDialogData = null;
    [SerializeField] SO_DialogDataBase _dynamicDialogData = null;
    public bool _isDisplayed = false;
    void Start()
    {
        int dialogIndex = -1;
        if (_staticDialogData != null)
            dialogIndex = _staticDialogData._dialogIndex;
        if (_dynamicDialogData != null)
            dialogIndex = _dynamicDialogData._dialogIndex;
        if (Managers.Game.playData.dialogComplete[(int)_stage].datas[dialogIndex])
            _isDisplayed = true;
        _staticDialogData?.Init();
        _dynamicDialogData?.Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player") && !_isDisplayed)
        {
            _isDisplayed = true;
            Managers.Game.playData.dialogComplete[(int)_stage].datas[_staticDialogData._dialogIndex] = true; 
            _staticDialogData.TransferDialogToUI();
            StartCoroutine(StartDynamic());
        }
    }

    IEnumerator StartDynamic()
    {
        yield return new WaitForSeconds(0.3f);
        _dynamicDialogData?.TransferDialogToUI();
    }
}
