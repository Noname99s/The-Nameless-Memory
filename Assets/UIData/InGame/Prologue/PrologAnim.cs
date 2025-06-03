using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PrologAnim : CutSceneAnimBase, ICutSceneSequenceBuilder
{
    [Header("Prolog Anim UI Setting")]
    [SerializeField] Image fadeScreen;

    [Header("Prolog Anim Settings")]
    [SerializeField] float fadeDuration = 2f;

    Sequence startAnimSequence;

    protected override void Start()
    {
        base.Start();
        BuildSequence();
        // Start the animation sequence
        startAnimSequence.Play();
    }

    public void BuildSequence()
    {
        startAnimSequence = DOTween.Sequence();
        // fade Squence
        startAnimSequence
            .Append(fadeScreen.DOFade(0.7f, fadeDuration).SetEase(Ease.InQuad))
            .Append(fadeScreen.DOFade(1f, fadeDuration).SetEase(Ease.InQuad))
            .Append(fadeScreen.DOFade(0.7f, fadeDuration).SetEase(Ease.InQuad))
            .Append(fadeScreen.DOFade(1f, fadeDuration).SetEase(Ease.InQuad))
            .Append(fadeScreen.DOFade(0f, fadeDuration).SetEase(Ease.InQuad))
            .OnComplete(() =>
            {
                if (!cutSceneFinished)
                    SequenceResult();
                else
                    Managers.Game.ChangeState(GameState.Home);
            })
            .onUpdate += () =>
            {
                if (Input.anyKeyDown)
                {
                    startAnimSequence.Pause();
                    fadeScreen.color = new Color(0, 0, 0, 0);
                    SequenceResult();
                }
            };
    }
    public void SequenceResult()
    {
        isAnimFinished = true;
        ScenarioSequence();
    }
    void OnDestroy()
    {
        startAnimSequence.Kill();
    }
}
