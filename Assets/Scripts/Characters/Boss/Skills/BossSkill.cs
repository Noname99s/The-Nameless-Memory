using Orpul1;
using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class BossSkill : MonoBehaviour,ISetDataAble<Action<GameObject>>,IResetAble
{
    [SerializeField] protected float delayToAttack;
    [SerializeField] protected float deativeTime;
    protected List<IObserver> observers = new List<IObserver>();
    protected Action<GameObject> enqueueAction;
    protected int damage;

    public abstract void Execute();
    public void AddObserver(IObserver observer)
    {
        if (observers.Contains(observer)) return;
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (!observers.Contains(observer)) return;
        observers.Remove(observer);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag(ETag.Player.ToString())) return;
        IDamageAble player = other as IDamageAble;
        if (player == null) return;
        player.OnDamage(damage);
    }

    public abstract void SetValue(Action<GameObject> t);

    public abstract void ResetValue();
}
