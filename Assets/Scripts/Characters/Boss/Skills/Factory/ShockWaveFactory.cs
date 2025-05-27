using UnityEngine;
using Orpul1;
using System;
using UnityEditor.Rendering;
namespace Orpul1.Boss.Mirrored
{
    public class ShockWaveFactory : BossSkillFactory
    {
        [SerializeField] Transform trPlayer;
        public override GameObject Create()
        {
            BossSkill skill = GetObject<BossSkill>();
            ResetSkill(skill);
            SetValue(skill);
            skill.transform.position = trPlayer.position;
            skill.Execute();
            return skill.gameObject;
        }
        private void ResetSkill(IResetAble skill)
        {
            skill.ResetValue();
        }
        private void SetValue(ISetDataAble<Action<GameObject>> skill)
        {
            skill.SetValue(SetUsingEndObject);
        }
    }
}
