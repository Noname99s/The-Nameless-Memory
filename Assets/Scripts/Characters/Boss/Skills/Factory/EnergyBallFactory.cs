using UnityEngine;
namespace Orpul1.Boss.Mirrored
{
    public class EnergyBallFactory : BossSkillFactory
    {
        [SerializeField] Transform[] spawnPosis;
        [SerializeField] Transform[] arrivePosis;
        public override GameObject Create()
        {
            for (int i = 0; i < 2; i++)
            {
                CreateSkill(i).Execute();
            }
            return null;
        }
        private BossSkill CreateSkill(int i)
        {
            BossSkill skill = GetObject<BossSkill>();

            ResetSkill(skill);
            SetValues(skill,i);
            return skill;
        }
        private void SetValues(BossSkill skill, int index)
        {
            SetSkillValue(skill);
            SetSkillSpawnPosi(skill, index);
            SetArrivePosi(skill as WatchOfRegretSkill, index);
        }
        private void SetArrivePosi(ISetDataAble<Vector2> skill, int index)
        {
            if (skill == null) return;
            skill.SetValue(arrivePosis[index].position);
        }
        private void SetSkillSpawnPosi(BossSkill skill, int spawnPosiIndex)
        {
            skill.transform.position = spawnPosis[spawnPosiIndex].position;
        }
    }
}