using System;
using UnityEngine;

public class InGameComposition : UIPopup
{
    [SerializeField] InGameView inGameView;
    InGamePresenter inGamePresenter;
    void Awake()
    {
        inGamePresenter = new InGamePresenter(inGameView);
    }
    void OnDestroy()
    {
        inGamePresenter.Dispose();
    }
    public void OnHpUpdateConnection(int hp, int maxHp)
    {
        inGameView.UpdateHP(hp, maxHp);
    }
}
