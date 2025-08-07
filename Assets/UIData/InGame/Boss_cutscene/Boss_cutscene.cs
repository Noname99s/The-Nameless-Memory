using DG.Tweening;
using Orpul1.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boss_cutscene : MonoBehaviour , ISequenceBuilder
{
    [SerializeField] DOTweenAnimation sequence;

    [Header("Ending Texts")]
    [SerializeField] Text displayText;
    Sequence sequenceText;
    List<string> endingTexts = new List<string>();
    BG_DB_Dialogtext endTexts;
    [Header("Clear Log")]
    [SerializeField] GameObject clearLogPanel;
    [SerializeField] TextMeshProUGUI clearPlayTime;
    [SerializeField] TextMeshProUGUI clearDeathCount;
    void Start()
    {
        BuildSequence();
    }
    public void OnCutSceneEnd()
    {
        sequence.DORestart();
    }
    public void OpenClearPanel()
    {
        displayText.text = string.Empty; 
        clearLogPanel.SetActive(true);
        TimeSpan timeSpan = TimeSpan.FromSeconds(Managers.Game.playData.playTime);
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        clearPlayTime.text = formattedTime;
        clearDeathCount.text = $"{Managers.Game.playData.playerStatus.deathCount}";
    }
    public void BackToMain()
    {
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
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
        endingTexts.Add(endTexts._DialogLines[1]._Texts[0]);
        endingTexts.Add(endTexts._DialogLines[1]._Texts[1]);
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
            .OnComplete(OpenClearPanel);
    }
}
