using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using Orpul1.UI;

public class InGameView : MonoBehaviour, IInGameView, ISequenceBuilder
{
    [Header("Dialog")]
    [SerializeField] DialogAnim bossDialogAnim;
    [SerializeField] DialogAnim dialogAnim;
    [SerializeField] Button staticDialog;
    [SerializeField] TextMeshProUGUI staticDialogText;
    int _currentStaticDialogIndex = 0;
    bool _isStaticDialogActive = false;
    List<string> _staticDialogTextList = new List<string>();
    [SerializeField] Text systemText;
    Sequence systemTextSequence;

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
    void Update()
    {
        if (Time.timeScale == 0 && staticDialog.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return)) 
        {
            DisplayDialog();
        }
    }
    private void OnDisable()
    {
        pausePanel.SetActive(false);
        staticDialog.gameObject.SetActive(false);
        dialogAnim.gameObject.SetActive(false);
        bossDialogAnim.gameObject.SetActive(false);
    }
    public void UpdateDialogAnim(List<string> text , bool isBoss = false)
    {
        if(!isBoss)
        {
            dialogAnim._textDatas.Clear();
            for(int i = 0; i < text.Count; i++)
            {
                dialogAnim._textDatas.Add(text[i]);
            }
            if (!dialogAnim.gameObject.activeSelf)
                dialogAnim.gameObject.SetActive(true);
            dialogAnim.PlayAnim();
        }
        else
        {
            bossDialogAnim._textDatas.Clear();
            for(int i = 0; i < text.Count; i++)
            {
                bossDialogAnim._textDatas.Add(text[i]);
            }
            if (!bossDialogAnim.gameObject.activeSelf)
                bossDialogAnim.gameObject.SetActive(true);
            bossDialogAnim.PlayAnim();
        }
    }
    public void UpdateStaticDialog(List<string> text)
    {
        Managers.Sound.PlayUISound((int)EUiSound.Popup);
        _currentStaticDialogIndex = 0;
        _staticDialogTextList = text;
        Time.timeScale = 0; 
        staticDialogText.text = text[_currentStaticDialogIndex++];
        staticDialog.gameObject.SetActive(true);
        _isStaticDialogActive = _currentStaticDialogIndex < _staticDialogTextList.Count; 
    }
    public void UpdateSystemText(string text)
    {
        Managers.Sound.PlayUISound((int)EUiSound.Notify);
        systemText.text = text;
        systemText.color = new Color(systemText.color.r, systemText.color.g, systemText.color.b, 0);
        systemTextSequence.Restart();
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

        systemTextSequence = DOTween.Sequence();
        systemTextSequence.OnStart(() => systemText.gameObject.SetActive(true))
                          .OnRewind(() => systemText.gameObject.SetActive(true))
                          .Append(systemText.DOFade(1, 0.5f).SetEase(Ease.OutQuad))
                          .AppendInterval(2f)
                          .Append(systemText.DOFade(0, 0.5f).SetEase(Ease.InQuad))
                          .OnComplete(() => systemText.gameObject.SetActive(false))
                          .SetAutoKill(false)
                          .Pause();
    }
    public void SequenceStart(Action action)
    {
        currentAction = action;
        fadeSequence.Restart();
    }

}
