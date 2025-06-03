using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SO_ScenarioData", menuName = "Scriptable Objects/SO_ScenarioData")]
public class SO_ScenarioData : ScriptableObject
{
    [SerializeField] string _scenarioType = "Text_Prologue";
    [HideInInspector] public List<string> _text = new List<string>();
    // Key : Enable use of scene sprites, Value : Sprite
    [HideInInspector] public Dictionary<int, Sprite> _sprites = new Dictionary<int, Sprite>();

    public void Init()
    {
        _text.Clear();
        _sprites.Clear();
        int listCount = BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines.Count;
        for (int i = 0; i < listCount; i++)
        {
            _text.Add(BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._Text);
            if (BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._SceneSprite != null)
            {
                _sprites.Add(i, BG_DB_Dialogtext.GetEntity(_scenarioType)._DialogLines[i]._SceneSprite);
            }
        }
    }
}
