using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class InGameView : MonoBehaviour, IInGameView, ISequenceBuilder
{
    [Header("Dialog")]
    [SerializeField] DialogAnim dialogAnim;
    [SerializeField] Button staticDialog;
    [SerializeField] TextMeshProUGUI staticDialogText;
    int _currentStaticDialogIndex = 0;
    bool _isStaticDialogActive = false;
    List<string> _staticDialogTextList = new List<string>();

    public GameObject pausePanel;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button openSaveLoadButton;
    [SerializeField] Button openOptionsButton;
    [SerializeField] Button backToMainMenuButton;
    [SerializeField] Button quitGameButton;
    [SerializeField] Image fadeImage;
    [SerializeField] TextMeshProUGUI playTimeText;
    Sequence fadeSequence;

    [Header("Player Stats")]
    [SerializeField] TextMeshProUGUI deathCountText;
    [SerializeField] Slider hpSlider;

    public Action currentAction; // Action to be invoked after fade-in
    public event Action OnPauseGame;
    public event Action OnResumeGame;
    public event Action OnOpenSaveLoad;
    public event Action OnOpenOptions;
    public event Action OnBackToMainMenu;
    public event Action OnQuitGame;
    public event Action OnEndStaticDialog;
    void Start()
    {
        pauseButton.onClick.AddListener
            (
                () =>
                {
                    OnPauseGame?.Invoke();
                    pausePanel.SetActive(true);
                    float playTime = Managers.Game.playData.playTime; 
                    TimeSpan timeSpan = TimeSpan.FromSeconds(playTime);
                    string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    playTimeText.text = formattedTime;
                }
            );
        resumeButton.onClick.AddListener
            (
                () =>
                {
                    OnResumeGame?.Invoke();
                    pausePanel.SetActive(false);
                }
            );
        openSaveLoadButton.onClick.AddListener(() => OnOpenSaveLoad?.Invoke());
        openOptionsButton.onClick.AddListener(() => OnOpenOptions?.Invoke());
        backToMainMenuButton.onClick.AddListener(() => OnBackToMainMenu?.Invoke());
        quitGameButton.onClick.AddListener(() => OnQuitGame?.Invoke());
        hpSlider.value = hpSlider.maxValue;
        deathCountText.text = "0"; // Initialize death count to 0
        staticDialog.onClick.AddListener(DisplayDialog);

        if (fadeSequence == null)
        {
            BuildSequence();
        }
    }
    public void UpdateDialogAnim(List<string> text)
    {
        dialogAnim._textDatas = text;
        if (!dialogAnim.gameObject.activeSelf)
            dialogAnim.gameObject.SetActive(true);
        dialogAnim.PlayAnim();
    }
    public void UpdateStaticDialog(List<string> text)
    {
        _currentStaticDialogIndex = 0;
        _staticDialogTextList = text;
        Time.timeScale = 0; 
        staticDialogText.text = text[_currentStaticDialogIndex++];
        staticDialog.gameObject.SetActive(true);
        _isStaticDialogActive = _currentStaticDialogIndex < text.Count; 
    }
    public void DisplayDialog()
    {
        if (_isStaticDialogActive)
        {
            staticDialogText.text = _staticDialogTextList[_currentStaticDialogIndex++];
            _isStaticDialogActive = _currentStaticDialogIndex < _staticDialogTextList.Count;
        }
        else
        {
            Time.timeScale = 1; 
            staticDialog.gameObject.SetActive(false);
        }
        OnEndStaticDialog?.Invoke();
    }
    public void UpdateDeathCount(int deathCount)
    {
        deathCountText.text = $"{deathCount}";
    }
    public void UpdateHP(int hp, int maxHp)
    {
        float targetValue = (float)hp / maxHp * hpSlider.maxValue;
        hpSlider.DOKill();
        hpSlider.DOValue(targetValue, 0.5f).SetEase(Ease.OutQuad);
    }
    public void BuildSequence()
    {
        fadeSequence = DOTween.Sequence();
        fadeSequence.OnStart(() => fadeImage.gameObject.SetActive(true))
                    .OnRewind(() => fadeImage.gameObject.SetActive(true))
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
