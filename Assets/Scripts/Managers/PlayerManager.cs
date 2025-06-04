using Orpul1.Player;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] SO_PlayerInfo playerInfo;
    public GameObject player;
    GameObject playerCamera;
    GameObject cameraViewBoundary;
    
    public void Init()
    {
        if (player == null)
        {
            player = Instantiate(playerInfo.playerPrefab, playerInfo.startPosition , Quaternion.identity);
            playerCamera = Instantiate(playerInfo.cameraPrefab);
            cameraViewBoundary = Instantiate(playerInfo.cameraViewBoundaryPrefab);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerCamera);
            DontDestroyOnLoad(cameraViewBoundary);
        }
    }
    public void ChangePlayerPos()
    {
        bool isHub = Managers.Game.CurrentState == GameState.Home;
        if (player == null) return;
        if (!isHub)
            player.transform.position = Managers.Map.playerSpawnPoint;
        else
        {
            player.transform.position = playerInfo.startPosition;
            player.GetComponent<PlayerController>().RecoverHp(isHub);
        }
        playerCamera.transform.position = player.transform.position;
    }
    public void SetActivePlayer(bool setActive)
    {
        if(player == null || playerCamera == null || cameraViewBoundary == null) return;
        player.SetActive(setActive);
        playerCamera.SetActive(setActive);
        cameraViewBoundary.SetActive(setActive);
    }
    public GameObject GetPlayer()
    {
        if (player == null) return null;
        return player;
    }
    public void RemovePlayer()
    {
        if (player == null) return;
        Destroy(player);
        Destroy(playerCamera);
        Destroy(cameraViewBoundary);
        player = null;
        playerCamera = null;
        cameraViewBoundary = null;
    }
}
