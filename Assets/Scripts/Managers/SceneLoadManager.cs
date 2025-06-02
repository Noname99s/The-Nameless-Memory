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
        loading = Managers.UI.ShowPopupUI<LoadingComposition>();
        async.completed += (AsyncOperation obj) =>
        {
            loading.LoadScene(sceneName);
        };
        currentScene = sceneName;
    }
}
