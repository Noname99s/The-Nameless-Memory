using Orpul1;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public abstract class BossSkill : MonoBehaviour,ISetDataAble<Action<GameObject>>,IResetAble
{
    [SerializeField] protected float delayToAttack;
    [SerializeField] protected float deativeTime;
    [SerializeField] protected int damage;
    [SerializeField] protected CharacterAttack attackCollider;
    [SerializeField] protected SoundController sound;
    protected List<IObserver> observers = new List<IObserver>();
    protected Action<GameObject> enqueueAction;

    public virtual void Execute()
    {
        ISetDataAble<int> attack = attackCollider as ISetDataAble<int>;
        if (attack == null) return;
        attack.SetValue(damage);
    }
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
        IDamageAble player = other.GetComponent<IDamageAble>();
        if (player == null) return;
        Debug.Log($"{name} damage to Player");
        player.OnDamage(damage);
    }

    public abstract void SetValue(Action<GameObject> t);

    public abstract void ResetValue();
}
