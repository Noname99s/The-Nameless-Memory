using UnityEngine;

[CreateAssetMenu(fileName = "SO_MapInfo", menuName = "Scriptable Objects/SO_MapInfo")]
public class SO_MapInfo : ScriptableObject
{
    public Vector2 entrancePoint; // The spawn point for the player in this stage
    public Vector2 exitPoint; // The exit point for the player in this stage
    public GameObject mapObject; // The objects that are part of this stage
}
