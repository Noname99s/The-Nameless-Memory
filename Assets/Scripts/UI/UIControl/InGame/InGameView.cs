using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;

public class InGameView : MonoBehaviour , IInGameView
{
    [SerializeField] DialogAnim dialogAnim;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button openOptionsButton;
    [SerializeField] Button backToMainMenuButton;
    [SerializeField] Button quitGameButton;

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

    }
    public void UpdateDeathCount(int deathCount)
    {
        deathCountText.text = $"{deathCount}";
    }
    public void UpdateHP(int hp, int maxHp)
    {
        hpSlider.DOValue((float)hp / maxHp, 0.5f).SetEase(Ease.OutQuad);
    }
}
