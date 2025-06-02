using UnityEngine;
using Orpul1;
using System;
namespace Orpul1.Boss.Mirrored
{
    public class ShockWaveFactory : BossSkillFactory
    {
        [SerializeField] Transform trPlayer;
        private void Start()
        {
            trPlayer = PlayerManager.Instance.GetPlayer().transform;
        }
        public override GameObject Create()
        {
            if (trPlayer == null) return null;

            BossSkill skill = GetObject<BossSkill>();
            ResetSkill(skill);
            SetSkillValue(skill);
            skill.transform.position = trPlayer.position;
            skill.Execute();
            return skill.gameObject;
        }
    }
}
