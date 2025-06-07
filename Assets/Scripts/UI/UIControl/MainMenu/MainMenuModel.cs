using UnityEngine;

public class MainMenuModel
{
    public void StartGame()
    {
        Managers.Game.playData.isStatusLoaded = false; 
        Managers.SceneLoad.LoadSceneQuick("Prologue");
    }
    public void LoadGame()
    {
        Managers.UI.ShowPopupUI<SaveLoadComposition>();
    }
    public void OpenOptions()
    {
        Managers.UI.ShowPopupUI<OptionComposition>();
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
#endif
    }
}
