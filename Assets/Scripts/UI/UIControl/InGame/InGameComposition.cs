using System;
using UnityEngine;
using System.Collections.Generic;

public class InGameComposition : UIPopup
{
    [SerializeField] InGameView inGameView;
    InGamePresenter inGamePresenter;
    void Awake()
    {
        inGamePresenter = new InGamePresenter(inGameView);
    }
    private void OnDisable()
    {
        inGameView.pausePanel.SetActive(false);
    }
    void OnDestroy()
    {
        inGamePresenter.Dispose();
    }
    public void OnDialogAnimUpdateConnection(List<string> text)
    {
        inGameView.UpdateDialogAnim(text);
    }
    public void OnStaticDialogUpdateConnection(List<string> text)
    {
        inGameView.UpdateStaticDialog(text);
    }
    public void OnDeathCountUpdateConnection(int deathCount)
    {
        inGameView.UpdateDeathCount(deathCount);
    }
    public void OnHpUpdateConnection(int hp, int maxHp)
    {
        inGameView.UpdateHP(hp, maxHp);
    }

    public void OnFadeInSequence(Action action)
    { 
        inGameView.SequenceStart(action);
    }
}
