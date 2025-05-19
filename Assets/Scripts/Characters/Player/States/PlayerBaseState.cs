using System;
using Orpul1.Player;
using Orpul1.State;
using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayerContext playerContext;
    protected Action<int> transitionTo;
    public PlayerBaseState(PlayerContext playerContext, Action<int> transitionTo)
    {
        this.playerContext = playerContext;
        this.transitionTo = transitionTo;
    }

    public abstract void OnExit();

    public abstract void OnStart();

    public abstract void OnUpdate();
}
