using UnityEngine;

public class PlayerManager : SingleTon<PlayerManager>
{
    GameObject player;
    private void OnDisable()
    {
        player = null;
    }
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
    public GameObject GetPlayer()
    {
        if (player == null) return null;
        return player;
    }
    public void RemovePlayer()
    {
        player = null;
    }
}
