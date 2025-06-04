
using System.Collections.Generic;
using UnityEngine;
using Orpul1.Enemy;
namespace Orpul1.Boss.Mirrored
{
    public class MonsterFactory : BossSkillFactory, IDeactiveAble
    {
        [SerializeField] List<EnemyFactory> monsterList = new List<EnemyFactory>();
        [SerializeField] Transform[] spawnPosis;

        public override GameObject Create()
        {
            int spawnPosi = Random.Range(0, spawnPosis.Length);

            SpawnEffect(spawnPosi);
            return SpawnEnemy(spawnPosi);
        }

        public void Deactive()
        {
            for (int i = 0; i < monsterList.Count; i++)
            {
                if (monsterList[i] == null) continue;
                monsterList[i].Deactive();
            }
        }

        private void SpawnEffect(int spawnPosi)
        {
            BossSkill skill = GetObject<BossSkill>();
            ResetSkill(skill);
            SetSkillValue(skill);
            skill.transform.position = spawnPosis[spawnPosi].position;
            skill.Execute();
        }

        private GameObject SpawnEnemy(int spawnPosi)
        {
            int monsterType = Random.Range(0, monsterList.Count);
            EnemyController enemy = monsterList[monsterType].Create<EnemyController>();
            enemy.transform.position = spawnPosis[spawnPosi].position;
            return enemy.gameObject;
        }
    }

}
