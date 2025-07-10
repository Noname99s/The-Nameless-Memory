using Orpul1;
using UnityEngine;
using System.Collections.Generic;
using Orpul1.Enemy;
using UnityEngine.Events;

public abstract class EnemyFactory : ObjectPool, IDeactiveAble
{
    protected UnityEvent deactiveEvents = new UnityEvent();
    public abstract T Create<T>();

    public abstract void Deactive();
    protected override void EnqueueObject(GameObject obj)
    {
        RemoveDieEvent(obj.GetComponent<IDieAble>());
        base.EnqueueObject(obj);
    }
    private void RemoveDieEvent(IDieAble enemy)
    {
        if (enemy == null) return;
        deactiveEvents.RemoveListener(enemy.Die);
    }
}
