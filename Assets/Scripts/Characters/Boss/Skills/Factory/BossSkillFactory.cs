using System;
using UnityEngine;

namespace Orpul1.Boss
{
    public abstract class BossSkillFactory : ObjectPool
    {
        public abstract GameObject Create();
        protected void SetUsingEndObject(GameObject obj)
        {
            EnqueueObject(obj);
        }
    }
}
