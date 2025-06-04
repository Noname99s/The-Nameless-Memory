using System;
using Orpul1.State;
using Orpul1.Enemy;

public abstract class EnemyState : IState
{
    protected EnemyContext context;
    protected Action<int> transitionTo;
    public EnemyState(EnemyContext context, Action<int> transitionTo)
    {
        this.context = context;
        this.transitionTo = transitionTo;
    }
    public abstract void OnStart();

    public abstract void OnUpdate();

    public abstract void OnExit();

    protected abstract void TransitionState();
}
