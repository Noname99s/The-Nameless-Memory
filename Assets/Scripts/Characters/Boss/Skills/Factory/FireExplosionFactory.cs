using System;
using Orpul1;
using UnityEngine;

namespace Orpul1.Boss.Mirrored
{
    public class FireExplosionFactory : BossSkillFactory
    {
        [SerializeField] Transform[] spawnPosis;
        [SerializeField] int index;
        public override GameObject Create()
        {
            index = UnityEngine.Random.Range(0, spawnPosis.Length);

            BossSkill skill = GetObject<BossSkill>();
            ResetSkill(skill);
            SetSkillValue(skill);
            skill.transform.position = spawnPosis[index].position;
            skill.Execute();
            return skill.gameObject;
        }
        private void ResetSkill(IResetAble skill)
        {
            skill.ResetValue();
        }
        private void SetSkillValue(ISetDataAble<Action<GameObject>> skill)
        {
            skill.SetValue(SetUsingEndObject);
        }
    }

}
