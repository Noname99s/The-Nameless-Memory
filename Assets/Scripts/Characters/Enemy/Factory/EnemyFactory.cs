using Orpul1;
using UnityEngine;
using System.Collections.Generic;
using Orpul1.Enemy;

public abstract class EnemyFactory : ObjectPool, IDeactiveAble
{
    public abstract T Create<T>();

    public void Deactive()
    {
        for (int i = 0; i < objList.Count; i++)
        {
            if (!objList[i].activeSelf) continue;
            IDamageAble damage = objList[i].GetComponent<IDamageAble>();
            if (damage == null) continue;
            damage.OnDamage(9999);
        }
    }
}
