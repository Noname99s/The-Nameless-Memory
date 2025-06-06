using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "SO_StaticDialogData", menuName = "Scriptable Objects/SO_StaticDialogData", order = 1)]
[Serializable]
public class SO_StaticDialogData : ScriptableObject
{
    [SerializeField] string _scenarioType = "Text_Prologue";
    public int _dialogIndex = 0;
    [SerializeField] int _startDialogIndex = 0;
    [SerializeField] int listSize = 1;

    [SerializeField] List<string> dialogTexts = new List<string>();
    event System.Action<List<string>> OnDialogUpdate;
    public void Init()
    {
        dialogTexts.Clear();
        for (int i = 0; i < listSize; i++)
        {
            dialogTexts.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i + _startDialogIndex]._Text);
        }
        OnDialogUpdate += Managers.UI.GetPopupUI<InGameComposition>().OnStaticDialogUpdateConnection;
    }
    public void TransferDialogToUI()
    {
        OnDialogUpdate?.Invoke(dialogTexts);
    }
}
