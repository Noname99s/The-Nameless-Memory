
using System.Collections.Generic;
using UnityEngine;
namespace Orpul1.Boss.Mirrored
{
    public class MonsterFactory : BossSkillFactory
    {
        [SerializeField] List<EnemyFactory> monsterList = new List<EnemyFactory>();
        [SerializeField] Transform[] spawnPosis;

        public override GameObject Create()
        {
            int monsterType = Random.Range(0, monsterList.Count);
            int spawnPosi = Random.Range(0, spawnPosis.Length);
            EnemyController enemy = monsterList[monsterType].Create<EnemyController>();
            enemy.transform.position = spawnPosis[spawnPosi].position;
            return enemy.gameObject;
        }
    }

}
