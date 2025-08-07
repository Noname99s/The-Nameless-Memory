using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct CutSceneData
{
    public Sprite _sceneSprite;
    public List<string> _text;
}
[CreateAssetMenu(fileName = "SO_ScenarioData", menuName = "Scriptable Objects/SO_ScenarioData")]
public class SO_ScenarioData : ScriptableObject
{
    
    [SerializeField] string _scenarioType = "Text_Prologue";
    
    public List<CutSceneData> _datas = new List<CutSceneData>();

    public void Init()
    {
        int listCount = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines.Count;

        for (int i = 0; i < listCount; i++)
        {
            CutSceneData data;
            data._sceneSprite = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._SceneSprite;
         
            data._text = new List<string>();
            int dataTextCount = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._Texts.Count;
            for (int j = 0; j < dataTextCount; j++)
            {
                data._text.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._Texts[j]);
            }
            _datas.Add(data);
        }
    }
}
