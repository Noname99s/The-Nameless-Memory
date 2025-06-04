using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;

public class InGameView : MonoBehaviour, IInGameView, ISequenceBuilder 
{
    [SerializeField] DialogAnim dialogAnim;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button openOptionsButton;
    [SerializeField] Button backToMainMenuButton;
    [SerializeField] Button quitGameButton;
    [SerializeField] Image fadeImage;
    Sequence fadeInSequence;
    Sequence fadeOutSequence;

    [Header("Player Stats")]
    [SerializeField] TextMeshProUGUI deathCountText;
    [SerializeField] Slider hpSlider;

    public event Action OnPauseGame;
    public event Action OnResumeGame;
    public event Action OnOpenOptions;
    public event Action OnBackToMainMenu;
    public event Action OnQuitGame;
    void Start()
    {
        pauseButton.onClick.AddListener(() => OnPauseGame?.Invoke());
        resumeButton.onClick.AddListener(() => OnResumeGame?.Invoke());
        openOptionsButton.onClick.AddListener(() => OnOpenOptions?.Invoke());
        backToMainMenuButton.onClick.AddListener(() => OnBackToMainMenu?.Invoke());
        quitGameButton.onClick.AddListener(() => OnQuitGame?.Invoke());
        //dialogText.text = string.Empty;
        hpSlider.value = hpSlider.maxValue;
        deathCountText.text = "0"; // Initialize death count to 0

        if (fadeInSequence == null || fadeOutSequence == null)
        {
            BuildSequence();
        }
    }
    public void UpdateDeathCount(int deathCount)
    {
        deathCountText.text = $"{deathCount}";
    }
    public void UpdateHP(int hp, int maxHp)
    {
        float targetValue = (float)hp / maxHp * hpSlider.maxValue;
        hpSlider.DOKill();

        hpSlider.DOValue(targetValue, 0.5f).SetEase(Ease.OutQuad).onComplete = () =>
        {
            if (hp <= 0)
            {
                dialogAnim.gameObject.SetActive(true);
            }
        };
    }
    public void BuildSequence()
    {
        fadeInSequence = DOTween.Sequence();
        fadeInSequence.Append(fadeImage.DOFade(1, 0.5f).SetEase(Ease.OutQuad))
                    .AppendInterval(2f)
                    .SetAutoKill(false)
                    .Pause();

        fadeOutSequence = DOTween.Sequence();
        fadeOutSequence.Append(fadeImage.DOFade(0, 0.5f).SetEase(Ease.InQuad))
                       .SetAutoKill(false)
                       .Pause();
    }
    public void SequenceStart(Action action)
    {
        fadeInSequence.OnComplete
            (
                () =>
                {
                    action?.Invoke(); // Invoke the action after fade-in completes
                    Color color = fadeImage.color;
                    color.a = 0; // Reset alpha to 0 after fade-in
                    fadeImage.color = color;
                }
            );
        fadeInSequence.Restart();
    }
    public void SequenceEnd(Action action)
    {
        fadeOutSequence.OnStart
            (
                () =>
                {
                    action?.Invoke(); // Invoke the action after fade-in completes
                }
            );
        fadeOutSequence.Restart();
    }
}
