using Orpul1;
using UnityEngine;
using System.Collections.Generic;

public abstract class EnemyFactory : ObjectPool
{
    public abstract T Create<T>();
}
