using UnityEngine;

public class ChooseRemindExitButton : MonoBehaviour
{
    public void OnClickExitButton()
    {
        Managers.Game.ChangeState(GameState.Home);
    }
}
