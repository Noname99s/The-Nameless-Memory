using System;
using UnityEngine;
using Orpul1.Boss.Mirrored;
using Orpul1.Boss;

[Serializable]
public struct PhaseCondition
{
    public float range1, range2;
}


[Serializable]
public class MirroredBossInfo
{
    public SpriteRenderer bossSprite;
    public Animator animator;
    public PhaseCondition[] phaseHpConditions;
    public EBossState bossState;
    public int hp;
}
[Serializable]
public class MirroredSkillContext
{
    public Transform[] spawnPoints;
    public GameObject[] skillPrefab;
}

[Serializable]
public class MirroredHitStateInfo
{
    public Color hitColor;
    public Color orginColor;
    public int blinkCount;
    public float blinkeDelay;
}
[Serializable]
public class MirroredSkillInfo
{
    public BossSkillFactory[] bossSkillFactories;
    public MirroredSkillContext[] skillContexts = new MirroredSkillContext[(int)EBossSkill.END];
    public float[] leftSkillCoolTimes = new float[(int)EBossSkill.END];
    public bool[] isSkillUse = new bool[(int)EBossSkill.END];
}
[Serializable]
public class MirroredBlackboardComponent
{
    public MirroredBossInfo bossInfo;
    public MirroredHitStateInfo hitStateInfo;
    public MirroredSkillInfo bossSkillInfo;
    /*
     
    /// ////////
    public BossSkillFactory[] bossSkillFactories;
    public MirroredSkillContext[] skillContexts = new MirroredSkillContext[(int)EBossSkill.END];
    public EBossState bossState;
    public SpriteRenderer bossSprite;
    public Animator animator;
    public Transform trPlayer;
    public PhaseCondition[] phaseHpConditions;
    public Color hitColor;
    public Color orginColor;
    public int blinkCount;
    public float blinkeDelay;
    public int hp;
    public float[] leftSkillCoolTimes = new float[(int)EBossSkill.END];
    public bool[] isSkillUse = new bool[(int)EBossSkill.END];
     */
}

[CreateAssetMenu(fileName = "MirroredBlackboard",menuName = "BehaviorNodes/Blackboard/MirroredBlackboard")]
public class MirroredBlackboard : ScriptableObject
{
	public MirroredBlackboardComponent btComponent;
}
