using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class CutSceneAnimBase : MonoBehaviour
{
    [Header("Data Setting")]
    [SerializeField] SO_ScenarioData scenarioData;

    [Header("CutScene UI Setting")]
    [SerializeField] Text scenarioText;
    [SerializeField] Image scenarioSprite;

    [Header("CutScene Text Anim Settings")]
    [SerializeField] float textDisplayDuration = 5f;
    [SerializeField] float textDisplayDelay = 2f;

    float timeCount = 0f;
    int currentScenarioIndex = 0;
    protected bool isAnimFinished = false;
    protected bool cutSceneFinished = false;

    protected virtual void Start()
    {
        scenarioData.Init();
    }
    protected virtual void Update()
    {
        if (isAnimFinished && !cutSceneFinished)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= textDisplayDelay)
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
            return;
        }
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
            })
            .onUpdate += () =>
            {
                if (Input.anyKeyDown)
                {
                    scenarioText.DOPause();
                    scenarioText.text = scenarioData._text[currentScenarioIndex - 1];
                    isAnimFinished = true;
                    timeCount = 0f;
                }
            };
    }

}
