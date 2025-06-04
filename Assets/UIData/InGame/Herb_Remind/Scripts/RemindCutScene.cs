using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RemindCutScene : CutSceneAnimBase , ISequenceBuilder , ISequnceResulter
{
    [Header("Remind Anim UI Setting")]
    [SerializeField] Image fadeScreen;

    [Header("Remind Anim Settings")]
    [SerializeField] float fadeDuration = 2f;
    Sequence startAnimSequence;
    [SerializeField] GameObject chooseRemind;

    protected override void Start()
    {
        base.Start();
        BuildSequence();
    }
    protected override void ScenarioSequence()
    {
        base.ScenarioSequence();
        if (cutSceneFinished)
        {
            // startAnimSequence거꾸로 재생
            startAnimSequence.Goto(startAnimSequence.Duration());
            startAnimSequence.PlayBackwards();
        }
    }
    public override void OnRestartSequnece()
    {
        base.OnRestartSequnece();
        chooseRemind.SetActive(false);
        this.gameObject.SetActive(true);
        startAnimSequence.Restart();
    }
    public void BuildSequence()
    {
        startAnimSequence = DOTween.Sequence();
        startAnimSequence
            .Append(fadeScreen.DOFade(0.7f, fadeDuration).SetEase(Ease.OutQuad))
            .Append(fadeScreen.DOFade(1f, fadeDuration).SetEase(Ease.OutQuad))
            .Append(fadeScreen.DOFade(0, fadeDuration).SetEase(Ease.OutQuad))
            .OnComplete(() =>
            {
                // finish play
                SequenceResult();
            })
            .SetAutoKill(false)
            .onUpdate += () =>
            {
                if (!cutSceneFinished && Input.anyKeyDown)
                {
                    startAnimSequence.Pause();
                    fadeScreen.color = new Color(0, 0, 0, 0);
                    SequenceResult();
                }
            };
        // finish playback
        startAnimSequence.onRewind += () =>
        {
            this.gameObject.SetActive(false);
            chooseRemind.SetActive(true);
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
