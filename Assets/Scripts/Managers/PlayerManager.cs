using Orpul1.Player;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] SO_PlayerInfo playerInfo;
    GameObject player;
    GameObject playerCamera;
    GameObject cameraViewBoundary;

    [SerializeField] Vector3 spawnPoint = Vector3.zero;
    public void Init()
    {
        if (player == null)
        {
            player = Instantiate(playerInfo.playerPrefab, playerInfo.startPosition, Quaternion.identity);
            playerCamera = Instantiate(playerInfo.cameraPrefab);
            cameraViewBoundary = Instantiate(playerInfo.cameraViewBoundaryPrefab);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerCamera);
            DontDestroyOnLoad(cameraViewBoundary);
        }
    }
    public void ChangePlayerPos(Vector3 pos = default(Vector3))
    {
        bool isHub = Managers.Game.CurrentState == GameState.Home;
        pos = isHub ? playerInfo.startPosition : pos;
        spawnPoint = pos;
        if (player == null) return;
        player.transform.position = pos;
        playerCamera.transform.position = player.transform.position;
        if (isHub)
            player.GetComponent<PlayerController>().RecoverHp(isHub);
    }
    public void SetActivePlayer(bool setActive)
    {
        if (player == null || playerCamera == null || cameraViewBoundary == null) return;
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
    public bool SetPosSuccess()
    {
        bool isSuccess = true;
        if (Managers.Game.CurrentState == GameState.Home || Managers.Game.CurrentState == GameState.Battle || Managers.Game.CurrentState == GameState.Boss)
        {
            isSuccess = player.transform.position == spawnPoint;
            if(!isSuccess)
                player.transform.position = spawnPoint; // Reset position if not successful
        }
        return isSuccess;
    }
}
