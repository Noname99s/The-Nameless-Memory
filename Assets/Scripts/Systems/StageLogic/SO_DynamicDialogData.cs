using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_DynamicDialogData", menuName = "Scriptable Objects/SO_DynamicDialogData", order = 1)]
public class SO_DynamicDialogData : SO_DialogDataBase
{
    event System.Action<List<string>> OnDialogUpdate;
    public override void Init()
    {
        base.Init();
        OnDialogUpdate += Managers.UI.GetPopupUI<InGameComposition>().OnDialogAnimUpdateConnection;
    }
    public override void TransferDialogToUI()
    {
        OnDialogUpdate?.Invoke(dialogTexts);
    }
    public override void ClearAction()
    {
        OnDialogUpdate -= Managers.UI.GetPopupUI<InGameComposition>().OnDialogAnimUpdateConnection;
    }
}
