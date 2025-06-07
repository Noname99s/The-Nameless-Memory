using Orpul1.UI;
using UnityEngine;

public class InGameModel
{
    public void PauseGame()
    {
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
        Debug.Log("게임 일시정지");
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Managers.Sound.PlayUISound((int)EUiSound.Cancel);
        Debug.Log("게임 재개");
        Time.timeScale = 1;
    }
    public void OpenSaveLoad()
    {
        Managers.Sound.PlayUISound((int)EUiSound.Popup);
        Managers.UI.ShowPopupUI<SaveLoadComposition>();
    }
    public void OpenOptions()
    {
        Managers.Sound.PlayUISound((int)EUiSound.Popup);
        Managers.UI.ShowPopupUI<OptionComposition>();
    }
    public void BackToMainMenu()
    {
        ResumeGame(); // 게임이 일시정지 상태라면 재개
        Managers.Game.ChangeState(GameState.MainMenu);
    }
    public void QuitGame()
    {
        Managers.Sound.PlayUISound((int)EUiSound.ButtonClick);
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
#endif
    }
}
