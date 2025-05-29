using System;
using Unity.IO.LowLevel.Unsafe;
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

        protected virtual void ResetSkill(IResetAble skill)
        {
            skill.ResetValue();
        }
        protected virtual void SetSkillValue(ISetDataAble<Action<GameObject>> skill)
        {
            skill.SetValue(SetUsingEndObject);
        }
    }
}
