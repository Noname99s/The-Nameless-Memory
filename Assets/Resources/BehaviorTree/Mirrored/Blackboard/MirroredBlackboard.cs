using System;
using System.Collections.Generic;
using UnityEngine;
using SOBehaviorTree;

[Serializable]
public class MirroredBlackboardComponent
{
	//Insert blackboard values
	public int a = 0;
}

[CreateAssetMenu(fileName = "MirroredBlackboard",menuName = "BehaviorNodes/Blackboard/MirroredBlackboard")]
public class MirroredBlackboard : ScriptableObject
{
	public MirroredBlackboardComponent btComponent;
}
