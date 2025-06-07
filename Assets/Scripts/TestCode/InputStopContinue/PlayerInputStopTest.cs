using Orpul1;
using UnityEngine;

public class PlayerInputStopTest : MonoBehaviour
{
    IProcessAble playerInputProcess;
    private void Awake()
    {
        playerInputProcess = Managers.Player.GetPlayer().GetComponent<IProcessAble>();
        
    }

    [ContextMenu("StopInput")]
    private void StopInput()
    {
        playerInputProcess.Process();
    }
    [ContextMenu("ContinueInput")]
    private void ContinueInput()
    {
        playerInputProcess.Abort();
    }
}
