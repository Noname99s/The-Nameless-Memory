using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Boss_cutscene : MonoBehaviour 
{
    [SerializeField] DOTweenAnimation sequence;

    [Header("Ending Texts")]
    [SerializeField] Text displayText;
    List<string> endingTexts = new List<string>();
    BG_DB_Dialogtext endTexts;

    public void BackToMain()
    {
        Managers.Game.ChangeState(GameState.MainMenu);
    }
    public void OnCutSceneEnd()
    {
        Debug.Log("Cutscene ended");
        sequence.DORestart();
    }
    public void OnFadeEnd()
    {
        endTexts = BG_DB_Dialogtext.GetEntity("Text_BossOutro");
        endingTexts.Clear();
        endingTexts.Add(endTexts._DialogLines[3]._Text);
        endingTexts.Add(endTexts._DialogLines[4]._Text);
        displayText.text = string.Empty;
        displayText.DOText(endingTexts[0], 2f, true)
            .SetEase(Ease.Linear)
            .OnComplete(() => 
            {
                displayText.text = string.Empty;
                displayText
                    .DOText(endingTexts[1], 2f, true)
                    .SetDelay(3f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => BackToMain());
            });
    }

}
