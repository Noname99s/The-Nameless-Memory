using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class SO_DialogDataBase : ScriptableObject
{
    [SerializeField] protected string _scenarioType = "Text_Prologue"; 
    public List<string> dialogTexts = new List<string>();
    public int _dialogIndex = 0;
    public virtual void Init()
    {
        dialogTexts.Clear();
        int dataTextCount = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[_dialogIndex]._Texts.Count;
        for (int i = 0; i < dataTextCount; i++)
        {
            dialogTexts.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[_dialogIndex]._Texts[i]);
        }
    }
    public abstract void TransferDialogToUI();
    public abstract void ClearAction();
}
