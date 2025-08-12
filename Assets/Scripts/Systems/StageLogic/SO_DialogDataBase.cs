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
        int listSize = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines.Count;
        for (int i = 0; i < listSize; i++)
        {
            int dataTextCount = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._Texts.Count;
            for (int j = 0; j < dataTextCount; j++)
            {
                dialogTexts.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._Texts[j]);
            }
        }
    }
    public abstract void TransferDialogToUI();
    public abstract void ClearAction();
}
