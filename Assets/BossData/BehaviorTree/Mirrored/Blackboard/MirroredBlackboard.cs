using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
using Orpul1;
using Orpul1.Boss;

[Serializable]
public class MirroredSkillContext
{
    public Transform[] spawnPoints;
    public GameObject[] skillPrefab;
}
[Serializable]
public class MirroredBlackboardComponent
{
    public BossSkillFactory[] bossSkillFactories;
    public MirroredSkillContext[] skillContexts = new MirroredSkillContext[(int)EBossSkill.END];
    public EBossState bossState;
    public Animator animator;
    public Transform trPlayer;
    public int[] phaseHpConditions;
    public int hp;
    public float[] leftSkillCoolTimes = new float[(int)EBossSkill.END];
    public bool[] isSkillUse = new bool[(int)EBossSkill.END];
}

[CreateAssetMenu(fileName = "MirroredBlackboard",menuName = "BehaviorNodes/Blackboard/MirroredBlackboard")]
public class MirroredBlackboard : ScriptableObject
{
	public MirroredBlackboardComponent btComponent;
}
