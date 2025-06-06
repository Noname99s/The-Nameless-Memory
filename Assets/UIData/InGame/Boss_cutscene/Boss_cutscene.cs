using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Boss_cutscene : MonoBehaviour , ISequenceBuilder
{
    [SerializeField] DOTweenAnimation sequence;

    [Header("Ending Texts")]
    [SerializeField] Text displayText;
    Sequence sequenceText;
    List<string> endingTexts = new List<string>();
    BG_DB_Dialogtext endTexts;
    void Start()
    {
        BuildSequence();
    }
    public void OnCutSceneEnd()
    {
        Debug.Log("Cutscene ended");
        sequence.DORestart();
    }
    public void BackToMain()
    {
        Managers.Game.ChangeState(GameState.MainMenu);
    }
    public void OnFadeEnd()
    {
        sequenceText.Restart();
    }
    public void BuildSequence()
    {
        endTexts = BG_DB_Dialogtext.GetEntity("Text_BossOutro");
        endingTexts.Clear();
        endingTexts.Add(endTexts._DialogLines[3]._Text);
        endingTexts.Add(endTexts._DialogLines[4]._Text);
        sequenceText = DOTween.Sequence();
        sequenceText
            .OnStart(() => 
            {
                displayText.text = string.Empty;
            })
            .Append(displayText.DOText(endingTexts[0], 2f, true).SetEase(Ease.Linear))
            .AppendInterval(3f)
            .AppendCallback(() => 
            {
                displayText.text = string.Empty; // Clear text before next display
            })
            .Append(displayText.DOText(endingTexts[1], 2f, true).SetEase(Ease.Linear))
            .AppendInterval(3f)
            .Pause()
            .OnComplete(BackToMain);
    }
}
