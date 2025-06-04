using Orpul1.Camera;
using Orpul1.Player;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] SO_PlayerInfo playerInfo;
    GameObject player;
    GameObject playerCamera;
    GameObject cameraViewBoundary;

    public void Init(Vector3 pos = default(Vector3))
    {
        if (player == null)
        {
            player = Instantiate(playerInfo.playerPrefab, playerInfo.startPosition, Quaternion.identity);
            playerCamera = Instantiate(playerInfo.cameraPrefab);
            cameraViewBoundary = Instantiate(playerInfo.cameraViewBoundaryPrefab);
        }
    }
    public void ChangePlayerPos(Vector3 pos = default(Vector3))
    {
        bool isHub = Managers.Game.CurrentState == GameState.Home;
        pos = isHub ? playerInfo.startPosition : pos;
        if (player == null) return;
        player.transform.position = pos;
        playerCamera.transform.position = player.transform.position;
        if (isHub)
            player.GetComponent<PlayerController>().RecoverHp(isHub);
    }
    public GameObject GetPlayer()
    {
        if (player == null) return null;
        return player;
    }
    public void RemovePlayer()
    {
        if (player == null) return;
        CameraManager.Instance.RemoveCamera((int)ECameraType.MainCamera);
        Destroy(player);
        Destroy(playerCamera);
        Destroy(cameraViewBoundary);
        player = null;
        playerCamera = null;
        cameraViewBoundary = null;
    }
}
