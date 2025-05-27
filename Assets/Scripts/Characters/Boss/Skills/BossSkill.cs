using Orpul1;
using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class BossSkill : MonoBehaviour, ISubject,ISetDataAble<Action<GameObject>>,IResetAble
{
    [SerializeField] protected float delayToAttack;
    [SerializeField] protected float deativeTime;
    protected List<IObserver> observers = new List<IObserver>();
    protected Action<GameObject> enqueueAction;

    public abstract void Execute();
    public void AddObserver(IObserver observer)
    {
        if (observers.Contains(observer)) return;
        observers.Add(observer);
    }
    public void Notify<T>(T t)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].Notify(t);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        if (!observers.Contains(observer)) return;
        observers.Remove(observer);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) { }

    public abstract void SetValue(Action<GameObject> t);

    public abstract void ResetValue();
}
