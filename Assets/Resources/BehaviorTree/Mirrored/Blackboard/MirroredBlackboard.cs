using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;
using Orpul1.Boss.Mirrored;


[Serializable]
public class MirroredBlackboardComponent
{
	//Insert blackboard values
	public EBossState bossState;
	public int[] phaseHpConditions;
	public int hp;
}

[CreateAssetMenu(fileName = "MirroredBlackboard",menuName = "BehaviorNodes/Blackboard/MirroredBlackboard")]
public class MirroredBlackboard : ScriptableObject
{
	public MirroredBlackboardComponent btComponent;
}
