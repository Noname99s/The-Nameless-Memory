using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "SO_StaticDialogData", menuName = "Scriptable Objects/SO_StaticDialogData", order = 1)]
[Serializable]
public class SO_StaticDialogData : SO_DialogDataBase
{
    event System.Action<List<string>> OnDialogUpdate;
    public override void Init()
    {
        base.Init();
        OnDialogUpdate += Managers.UI.GetPopupUI<InGameComposition>().OnStaticDialogUpdateConnection;
    }
    public override void TransferDialogToUI()
    {
        OnDialogUpdate?.Invoke(dialogTexts);
    }
    public override void ClearAction()
    {
        OnDialogUpdate -= Managers.UI.GetPopupUI<InGameComposition>().OnStaticDialogUpdateConnection;
    }
}
