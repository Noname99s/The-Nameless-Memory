using UnityEngine;

[CreateAssetMenu(fileName = "SO_PlayerInfo", menuName = "Scriptable Objects/SC_PlayerInfo")]
public class SO_PlayerInfo : ScriptableObject
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public Vector3 startPosition = new Vector3(9,8,0); // Default starting position
}
