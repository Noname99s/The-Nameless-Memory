using System;
using UnityEngine;
using Orpul1.Boss.Mirrored;
using Orpul1.Boss;
using Orpul1.Camera;

[Serializable]
public struct PhaseCondition
{
    public float range1, range2;
}


[Serializable]
public class MirroredBossInfo
{
    public SpriteRenderer bossSprite;
    public GameObject bossObj;
    public Animator animator;
    public PhaseCondition[] phaseHpConditions;
    public EBossState bossState;
    public int hp;
    public bool shouldIntro = true;
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
    public ICameraEffectAble cameraEffect;
    public float[] leftSkillCoolTimes = new float[(int)EBossSkill.END];
    public bool[] isSkillUse = new bool[(int)EBossSkill.END];
}
[Serializable]
public class MirroredBlackboardComponent
{
    public MirroredBossInfo bossInfo;
    public MirroredHitStateInfo hitStateInfo;
    public MirroredSkillInfo bossSkillInfo;
    public SoundController sound;
}

[CreateAssetMenu(fileName = "MirroredBlackboard",menuName = "BehaviorNodes/Blackboard/MirroredBlackboard")]
public class MirroredBlackboard : ScriptableObject
{
	public MirroredBlackboardComponent btComponent;
}
