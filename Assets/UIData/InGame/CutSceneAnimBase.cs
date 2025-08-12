using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class CutSceneAnimBase : MonoBehaviour
{
    [Header("Data Setting")]
    [SerializeField] SO_ScenarioData scenarioData;

    [Header("CutScene UI Setting")]
    [SerializeField] protected Text scenarioText;
    [SerializeField] Image scenarioSprite;
    [SerializeField] Image stopFade;
    [SerializeField] GameObject stopText;

    [Header("CutScene Text Anim Settings")]
    [SerializeField] float textDisplayDuration = 5f;
    [SerializeField] float textDisplayDelay = 2f;

    float timeCount = 0f;
    int cutSceneIndex = 0;
    int textIndex = 0;
    protected bool isAnimFinished = false;
    protected bool cutSceneFinished = false;
    protected bool stopCutScene = false;

    protected virtual void Awake()
    {
        scenarioData.Init();
    }
    void Update()
    {
        if (cutSceneFinished) return;
        
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            cutSceneFinished = true;
            isAnimFinished = true;

            stopCutScene = false;
            stopFade.color = new Color(0, 0, 0, 0);
            stopText.SetActive(false);
            Time.timeScale = 1f;

            scenarioText.DOKill();
            ScenarioSequence(); 
        }

        if (!isAnimFinished && !stopCutScene && Input.GetMouseButtonDown(0))
        {
            scenarioText.DOPause();
            scenarioText.text = scenarioData._datas[cutSceneIndex]._text[textIndex++];
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
        textIndex = 0;
        cutSceneIndex = 0;
        isAnimFinished = false;
        cutSceneFinished = false;
        if(scenarioData != null)
        {
            scenarioSprite.sprite = scenarioData._datas[cutSceneIndex]._sceneSprite;
        }
        scenarioText.text = string.Empty;
    }
    protected virtual void ScenarioSequence()
    {
        // Check if the cut scene Text finished
        if (textIndex >= scenarioData._datas[cutSceneIndex]._text.Count)
        {
            cutSceneIndex++;
            textIndex = 0;
        }
        // Check if the cut scene is finished
        if (cutSceneIndex >= scenarioData._datas.Count)
        {
            cutSceneFinished = true;
        }
        if (cutSceneFinished) return;

        // Reset the time count and text index for the next cut scene
        timeCount = 0f;
        isAnimFinished = false;

        // Update the scenario sprite and text
        scenarioSprite.sprite = scenarioData._datas[cutSceneIndex]._sceneSprite;
        scenarioText.text = string.Empty;
        scenarioText.DOText(scenarioData._datas[cutSceneIndex]._text[textIndex], textDisplayDuration, true)
            .SetEase(Ease.Linear)
            .SetLoops(1, LoopType.Restart)
            .SetAutoKill(false)
            .OnComplete(() =>
            {
                textIndex++;
                isAnimFinished = true;
            });
    }
}
