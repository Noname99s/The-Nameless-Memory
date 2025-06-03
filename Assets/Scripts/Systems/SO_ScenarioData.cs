using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SO_ScenarioData", menuName = "Scriptable Objects/SO_ScenarioData")]
public class SO_ScenarioData : ScriptableObject
{
    [SerializeField] string _scenarioType = "Text_Prologue";
    [HideInInspector] public List<string> _text = new List<string>();
    [HideInInspector] public List<Sprite> _sprites = new List<Sprite>();

    public void Init()
    {
        _text.Clear();
        _sprites.Clear();
        int listCount = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines.Count;
        for (int i = 0; i < listCount; i++)
        {
            _text.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._Text);
            _sprites.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._SceneSprite);
        }
    }
}
