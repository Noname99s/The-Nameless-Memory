using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PrologAnim : MonoBehaviour
{
    Sequence prologAnimSequence;
    [SerializeField] Image fadeScreen;
    [SerializeField] Text prologText;
    [SerializeField] Image prologImg;
    [SerializeField] string[] scenarioText = new string[5];
    [SerializeField] Sprite[] scenarioImages = new Sprite[5];

    [Header("Prolog Anim Settings")]
    [SerializeField] float fadeDuration = 2f;
    [SerializeField] float textDisplayDuration = 5f;
    [SerializeField] float textDisplayDelay = 2f;

    int currentScenarioIndex = 0;
    bool isPrologAnimFinished = false;
    float timeCount = 0f;

    void Start()
    {
        // Build the animation sequence
        BuilSequence();
        
        // Start the animation sequence
        prologAnimSequence.Play();
    }
    private void Update()
    {
        if(isPrologAnimFinished)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= textDisplayDelay || Input.anyKeyDown)
            {
                ScenarioSequence();
                timeCount = 0f;
            }
        }
    }
    void ScenarioSequence()
    {
        if (currentScenarioIndex >= scenarioText.Length)
        {
            return;
        }
        isPrologAnimFinished = false;
        prologImg.sprite = scenarioImages[currentScenarioIndex];
        prologText.text = string.Empty;
        prologText.DOText(scenarioText[currentScenarioIndex++], textDisplayDuration, true)
            .SetEase(Ease.Linear)
            .SetLoops(1, LoopType.Restart)
            .OnComplete(() =>
            {
                isPrologAnimFinished = true;
            });
    }

    void BuilSequence()
    {
        prologAnimSequence = DOTween.Sequence();
        // fade Squence
        prologAnimSequence.Append(fadeScreen.DOFade(0.7f, fadeDuration).SetEase(Ease.InQuad));
        prologAnimSequence.Append(fadeScreen.DOFade(1f, fadeDuration).SetEase(Ease.InQuad));
        prologAnimSequence.Append(fadeScreen.DOFade(0.7f, fadeDuration).SetEase(Ease.InQuad));
        prologAnimSequence.Append(fadeScreen.DOFade(1f, fadeDuration).SetEase(Ease.InQuad));
        prologAnimSequence.Append(fadeScreen.DOFade(0f, fadeDuration).SetEase(Ease.InQuad));

        prologAnimSequence.OnComplete(() =>
        {
            isPrologAnimFinished = true;
            ScenarioSequence();
        });
    }
}
