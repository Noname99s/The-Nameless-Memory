using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager 
{
    public string currentScene;
    LoadingComposition loading;
    public void Init()
    {
        currentScene = "MainMenu";
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        AsyncOperation async = SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
        async.completed += (AsyncOperation obj) => { SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene)); };
    }
    public void LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.UnloadSceneAsync(currentScene);
        async.completed += (AsyncOperation obj) =>
        {
            Managers.UI.CloseAllPopupUI(); // Close all UI popups before loading new scene
            loading = Managers.UI.ShowPopupUI<LoadingComposition>();
            loading.LoadScene(sceneName);
        };
        currentScene = sceneName;
    }
}
