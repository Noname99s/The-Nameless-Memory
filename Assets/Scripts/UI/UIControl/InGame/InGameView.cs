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
    Sequence fadeSequence;

    [Header("Player Stats")]
    [SerializeField] TextMeshProUGUI deathCountText;
    [SerializeField] Slider hpSlider;

    public Action currentAction; // Action to be invoked after fade-in
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

        if (fadeSequence == null)
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
        fadeSequence = DOTween.Sequence();
        fadeSequence.OnRewind(() => fadeImage.gameObject.SetActive(true))
                    .Append(fadeImage.DOFade(1, 0.5f).SetEase(Ease.OutQuad))
                    .AppendCallback(() => currentAction?.Invoke())
                    .AppendInterval(2f)
                    .Append(fadeImage.DOFade(0, 0.5f).SetEase(Ease.InQuad))
                    .OnComplete(() => fadeImage.gameObject.SetActive(false))
                    .SetAutoKill(false)
                    .Pause();
    }
    public void SequenceStart(Action action)
    {
        currentAction = action;
        fadeSequence.Restart();
    }

}
