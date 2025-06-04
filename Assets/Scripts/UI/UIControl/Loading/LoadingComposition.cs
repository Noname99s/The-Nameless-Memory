using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingComposition : UIPopup
{
    [SerializeField] Text loadingText;
    BG_DB_LoadingText loadingTextData;
    public string nextScene;
    private void OnEnable()
    {
        loadingText.text = string.Empty; 
        loadingTextData = BG_DB_LoadingText.GetEntity(Random.Range(0, 4));

        if (loadingText != null)
        {
            loadingText.DOText(loadingTextData._detail + "\n" + loadingTextData._name, 3f, true)
                .SetEase(Ease.Linear)
                .SetLoops(1, LoopType.Restart);
        }
    }

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;
        asyncLoad.completed += (AsyncOperation obj) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
            Managers.Game.SettingScene();
            Managers.UI.ClosePopupUI<LoadingComposition>();
        };

        float time = Time.time + 3.0f; // 3 seconds term


        while (!asyncLoad.isDone)
        {
            yield return null;
            {
                if (asyncLoad.progress >= 0.9f)
                {
                    if (Time.time > time)
                    {
                        asyncLoad.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}
