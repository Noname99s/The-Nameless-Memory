using UnityEngine;
using Orpul1;
using System;
namespace Orpul1.Boss.Mirrored
{
    public class ShockWaveFactory : BossSkillFactory
    {
        [SerializeField] Transform trPlayer;
        public override GameObject Create()
        {
            BossSkill skill = GetObject<BossSkill>();
            ResetSkill(skill);
            SetSkillValue(skill);
            skill.transform.position = trPlayer.position;
            skill.Execute();
            return skill.gameObject;
        }
    }
}
