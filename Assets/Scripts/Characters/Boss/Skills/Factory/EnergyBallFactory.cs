using UnityEngine;
using Orpul1;
using System;
namespace Orpul1.Boss.Mirrored
{
    public class EnergyBallFactory : BossSkillFactory
    {
        public override GameObject Create()
        {
            BossSkill skill = GetObject<BossSkill>();
            ResetSkill(skill);
            SetValue(skill);
            return skill.gameObject;
        }
        private void SetValue(ISetDataAble<Action<GameObject>> skill)
        {
            skill.SetValue(SetUsingEndObject);
        }
        private void ResetSkill(IResetAble skill)
        {
            skill.ResetValue();
        }

    }
}