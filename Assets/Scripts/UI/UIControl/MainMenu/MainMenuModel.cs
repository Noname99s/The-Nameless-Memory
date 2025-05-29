using UnityEngine;

public class MainMenuModel
{
    public void StartGame()
    {
        // 게임 시작 로직을 여기에 추가
        Debug.Log("게임 시작");
        Managers.SceneLoad.LoadScene("PlayTest");
    }
    public void LoadGame()
    {
        // 게임 로드 로직을 여기에 추가
        Debug.Log("게임 로드");
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
