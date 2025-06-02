using Orpul1.Player;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] SO_PlayerInfo playerInfo;
    GameObject player;
    GameObject playerCamera;

    
    public void Init(bool isHub)
    {
        if (player == null)
        {
            player = Managers.Resource.Instantiate(playerInfo.playerPrefab);
            playerCamera = Managers.Resource.Instantiate(playerInfo.cameraPrefab);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerCamera);
        }
        StartCoroutine(MoveSpawnPos(isHub));
    }
    IEnumerator MoveSpawnPos(bool isHub)
    {
        yield return new WaitForSeconds(0.1f);
        player.SetActive(true);
        playerCamera.SetActive(true);
        if (isHub)
            player.transform.position = playerInfo.startPosition;
        else
            player.transform.position = Managers.Map.playerSpawnPoint;
        player.GetComponent<PlayerController>().RecoverHp(isHub);
    }
    public void MoveStage()
    {
        if(player == null || playerCamera == null) return;
        player.SetActive(false);
        playerCamera.SetActive(false);
    }
    public GameObject GetPlayer()
    {
        if (player == null) return null;
        return player;
    }
    public void RemovePlayer()
    {
        if (player == null) return;
        Destroy(playerCamera);
        Destroy(player);
        player = null;
    }
}
