using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;

public class InGameView : UIPopup , IInGameView
{
    [SerializeField] DialogAnim dialogAnim;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button openOptionsButton;
    [SerializeField] Button backToMainMenuButton;
    [SerializeField] Button quitGameButton;

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
}
