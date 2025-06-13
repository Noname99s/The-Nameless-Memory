using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class CutSceneAnimBase : MonoBehaviour
{
    [Header("Data Setting")]
    [SerializeField] SO_ScenarioData scenarioData;

    [Header("CutScene UI Setting")]
    [SerializeField]protected Text scenarioText;
    [SerializeField] Image scenarioSprite;
    [SerializeField] Image stopFade;
    [SerializeField] GameObject stopText;

    [Header("CutScene Text Anim Settings")]
    [SerializeField] float textDisplayDuration = 5f;
    [SerializeField] float textDisplayDelay = 2f;

    float timeCount = 0f;
    int currentScenarioIndex = 0;
    protected bool isAnimFinished = false;
    protected bool cutSceneFinished = false;
    protected bool stopCutScene = false;

    protected virtual void Start()
    {
        scenarioData.Init();
    }
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!stopCutScene)
            {
                stopCutScene = true;
                stopFade.color = new Color(0, 0, 0, 0.75f);
                stopText.SetActive(true);
                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                stopCutScene = false;
                stopFade.color = new Color(0, 0, 0, 0); 
                stopText.SetActive(false);
                Time.timeScale = 1f; // Resume the game
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            cutSceneFinished = true;
            isAnimFinished = true;
            Time.timeScale = 1f;
            scenarioText.DOKill();
        }

        if (!cutSceneFinished && !stopCutScene && Input.GetMouseButtonDown(0))
        {
            scenarioText.DOPause();
            scenarioText.text = scenarioData._text[currentScenarioIndex - 1];
            isAnimFinished = true;
        }
        
        if (isAnimFinished)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= textDisplayDelay || cutSceneFinished)
            {
                ScenarioSequence();
            }
        }
    }
    public virtual void OnRestartSequnece()
    {
        timeCount = 0f;
        currentScenarioIndex = 0;
        isAnimFinished = false;
        cutSceneFinished = false;
        if(scenarioData != null && scenarioData._sprites.ContainsKey(currentScenarioIndex))
        {
            scenarioSprite.sprite = scenarioData._sprites[currentScenarioIndex];
        }
        scenarioText.text = string.Empty;
    }
    protected virtual void ScenarioSequence()
    {
        if (currentScenarioIndex >= scenarioData._text.Count)
        {
            cutSceneFinished = true;
        }
        if (cutSceneFinished) return;
        timeCount = 0f;
        isAnimFinished = false;

        if (scenarioData._sprites.ContainsKey(currentScenarioIndex))
        {
            scenarioSprite.sprite = scenarioData._sprites[currentScenarioIndex];
        }
        scenarioText.text = string.Empty;
        scenarioText.DOText(scenarioData._text[currentScenarioIndex++], textDisplayDuration, true)
            .SetEase(Ease.Linear)
            .SetLoops(1, LoopType.Restart)
            .SetAutoKill(false)
            .OnComplete(() =>
            {
                isAnimFinished = true;
            });
    }

}
