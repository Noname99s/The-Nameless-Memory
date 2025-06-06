using System.Collections.Generic;
using UnityEngine;

public class DynamicDialogData : MonoBehaviour
{
    [SerializeField] string _scenarioType = "Text_Prologue";
    [SerializeField] int _startDialogIndex = 0;
    [SerializeField] int listSize = 0;

    [SerializeField] List<string> dialogTexts = new List<string>();
    event System.Action<List<string>> OnDialogUpdate;
    void Start()
    {
        for (int i = 0; i < listSize; i++)
        {
            dialogTexts.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i + _startDialogIndex]._Text);
        }
        OnDialogUpdate += Managers.UI.GetPopupUI<InGameComposition>().OnDialogAnimUpdateConnection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TransferDialogToUI();
        }

    }
    public void TransferDialogToUI()
    {
        OnDialogUpdate?.Invoke(dialogTexts);
    }
}
