using Orpul1.State;
using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayerContext playerContext;
    public PlayerBaseState(PlayerContext playerContext)
    {
        this.playerContext = playerContext;
    }

    public abstract void OnExit();

    public abstract void OnStart();

    public abstract void OnUpdate();
}
