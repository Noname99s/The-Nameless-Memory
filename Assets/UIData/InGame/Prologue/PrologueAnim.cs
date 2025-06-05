using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PrologueAnim : CutSceneAnimBase, ISequenceBuilder , ISequnceResulter
{
    [Header("Prologue Anim UI Setting")]
    [SerializeField] Image fadeScreen;

    [Header("Prologue Anim Settings")]
    [SerializeField] float fadeDuration = 2f;

    Sequence startAnimSequence;

    protected override void Start()
    {
        base.Start();
        BuildSequence();
        // Start the animation sequence
        startAnimSequence.Play();
    }
    protected override void ScenarioSequence()
    {
        base.ScenarioSequence();
        if(cutSceneFinished)
            Managers.Game.ChangeState(GameState.Battle);
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
                SequenceResult();
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
