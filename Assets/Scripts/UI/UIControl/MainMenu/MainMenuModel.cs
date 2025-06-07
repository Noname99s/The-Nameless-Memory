using Orpul1.UI;
using UnityEngine;

public class MainMenuModel
{
    public void StartGame()
    {
        Managers.Game.playData.isStatusLoaded = false; 
        Managers.SceneLoad.LoadSceneQuick("Prologue");
        Managers.Sound.PlayUISound(EUiSound.ButtonClick);
    }
    public void LoadGame()
    {
        Managers.UI.ShowPopupUI<SaveLoadComposition>();
        Managers.Sound.PlayUISound(EUiSound.ButtonClick);
    }
    public void OpenOptions()
    {
        Managers.UI.ShowPopupUI<OptionComposition>();
        Managers.Sound.PlayUISound(EUiSound.ButtonClick);
    }
    public void QuitGame()
    {
        Managers.Sound.PlayUISound(EUiSound.ButtonClick);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
#endif
    }
}
