using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_DynamicDialogData", menuName = "Scriptable Objects/SO_DynamicDialogData", order = 1)]
public class SO_DynamicDialogData : ScriptableObject
{
    [SerializeField] string _scenarioType = "Text_Prologue";
    public int _dialogIndex = 0;
    [SerializeField] int _startDialogIndex = 0;
    [SerializeField] int listSize = 1;

    public List<string> dialogTexts = new List<string>();
    event System.Action<List<string>> OnDialogUpdate;
    public void Init()
    {
        dialogTexts.Clear();
        for (int i = 0; i < listSize; i++)
        {
            dialogTexts.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i + _startDialogIndex]._Text);
        }
        OnDialogUpdate += Managers.UI.GetPopupUI<InGameComposition>().OnDialogAnimUpdateConnection;
    }
    public void TransferDialogToUI()
    {
        OnDialogUpdate?.Invoke(dialogTexts);
    }
    public void ClearAction()
    {
        OnDialogUpdate -= Managers.UI.GetPopupUI<InGameComposition>().OnDialogAnimUpdateConnection;
    }
}
