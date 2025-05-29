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
    public void SubScribeHpUpdate(Action<int, int> action)
    {
        action += inGameView.UpdateHP;
    }
    public void SubScribeDeathCountUpdate(Action<int> action)
    {
        action += inGameView.UpdateDeathCount;
    }
    public void UnSubScribeHpUpdate(Action<int, int> action)
    {
        action -= inGameView.UpdateHP;
    }
    public void UnSubScribeDeathCountUpdate(Action<int> action)
    {
        action -= inGameView.UpdateDeathCount;
    }

}
