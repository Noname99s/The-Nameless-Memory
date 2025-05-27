using UnityEngine;
using Orpul1;
using UnityEditor.Experimental.GraphView;
namespace Orpul1.Boss.Mirrored
{
    public class LaserFactory : BossSkillFactory
    {
        [SerializeField] Transform[] spawnPosi;

        public override GameObject Create()
        {
            for (int i = 0; i < spawnPosi.Length; i++)
            {
                BossSkill skill = GetObject<BossSkill>();
                SetSkill(skill, i);
                skill.Execute();
            }
            return null;
        }
        private void SetSkill(BossSkill skill, int index)
        {
            skill.transform.position = spawnPosi[index].position;
            ResetSkill(skill);
            SetSkillValue(skill);
        }
        
    }
}
