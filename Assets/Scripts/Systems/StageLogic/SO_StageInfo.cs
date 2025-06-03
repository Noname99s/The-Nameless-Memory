using UnityEngine;

[CreateAssetMenu(fileName = "SO_StageInfo", menuName = "Scriptable Objects/SO_StageInfo")]
public class SO_StageInfo : ScriptableObject
{
    public Vector2 spawnPoint; // The spawn point for the player in this stage
    public Stage stageIndex;
    public GameObject[] stageObjects; // The objects that are part of this stage
}
