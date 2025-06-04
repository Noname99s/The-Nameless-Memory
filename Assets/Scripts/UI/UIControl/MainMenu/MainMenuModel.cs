using UnityEngine;

public class MainMenuModel
{
    public void StartGame()
    {
        Managers.SceneLoad.LoadSceneQuick("Prologue");
    }
    public void LoadGame()
    {
        // 게임 로드 로직을 여기에 추가
        Debug.Log("게임 로드");
        Managers.Game.ChangeState(GameState.Boss);
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
