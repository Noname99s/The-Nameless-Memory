using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MainMenuView : UIPopup , IMainMenuView
{

    [SerializeField] DOTweenAnimation[] dOTweenAnimations;
    [SerializeField] Button startButton;
    [SerializeField] Button loadButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;

    public event Action OnStartGame;
    public event Action OnLoadGame;
    public event Action OnOpenOptions;
    public event Action OnQuitGame;

    void Start()
    {
        startButton.onClick.AddListener(() => OnStartGame?.Invoke());
        loadButton.onClick.AddListener(() => OnLoadGame?.Invoke());
        optionsButton.onClick.AddListener(() => OnOpenOptions?.Invoke());
        quitButton.onClick.AddListener(() => OnQuitGame?.Invoke());
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            for (int i = 0; i < dOTweenAnimations.Length; i++)
            {
                if (dOTweenAnimations[i].isActiveAndEnabled)
                {
                    dOTweenAnimations[i].DOPlay();
                }
            }
        }
    }
}
