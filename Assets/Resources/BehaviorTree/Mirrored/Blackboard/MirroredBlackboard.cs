using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;
using Orpul1;

[Serializable]
public class MirroredSkillContext
{
    public Transform[] spawnPoints;
    public GameObject[] skillPrefab;
}
[Serializable]
public class MirroredBlackboardComponent
{
    //Insert blackboard values
    public MirroredSkillContext[] skillContexts = new MirroredSkillContext[(int)EBossSkill.END];
    public EBossState bossState;
    public Animator animator;
    public int[] phaseHpConditions;
    public int hp;
    public float[] leftSkillCoolTimes = new float[(int)EBossSkill.END];
    public float[] defaultSkillCoolTimes = new float[(int)EBossSkill.END];
    public bool[] isSkillUse = new bool[(int)EBossSkill.END];
    public IObserver bossObserver;
}

[CreateAssetMenu(fileName = "MirroredBlackboard",menuName = "BehaviorNodes/Blackboard/MirroredBlackboard")]
public class MirroredBlackboard : ScriptableObject
{
	public MirroredBlackboardComponent btComponent;
}
