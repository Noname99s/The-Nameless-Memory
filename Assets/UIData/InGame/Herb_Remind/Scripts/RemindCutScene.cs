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


    protected override void Awake()
    {
        base.Awake();
        BuildSequence();
    }
    protected override void ScenarioSequence()
    {
        base.ScenarioSequence();
        if (cutSceneFinished)
        {
            startAnimSequence.Goto(startAnimSequence.Duration());
            startAnimSequence.PlayBackwards();
        }
    }
    public override void OnRestartSequnece()
    {
        this.gameObject.SetActive(true);
        chooseRemind.SetActive(false);
        base.OnRestartSequnece();
        startAnimSequence.Restart();
    }
    public void BuildSequence()
    {
        startAnimSequence = DOTween.Sequence();
        startAnimSequence
            .Append(fadeScreen.DOFade(0.7f, fadeDuration).SetEase(Ease.OutQuad))
            .Append(fadeScreen.DOFade(1f, fadeDuration).SetEase(Ease.OutQuad))
            .Append(fadeScreen.DOFade(0, fadeDuration).SetEase(Ease.OutQuad))
            .OnComplete(() => // playback 할 경우 OnComplete가 OnStart로 호출됨
            {
                // finish play
                SequenceResult();
            })
            .SetAutoKill(false)
            .onUpdate += () =>
            {
                if (!cutSceneFinished && Input.GetMouseButtonDown(0))
                {
                    startAnimSequence.Pause();
                    fadeScreen.color = new Color(0, 0, 0, 0);
                    SequenceResult();
                }
            };
        // finish playback
        startAnimSequence.onRewind += () =>
        {
            isAnimFinished = false;
            cutSceneFinished = false;
            this.gameObject.SetActive(false);
            chooseRemind.SetActive(true);
        };
    }
    public void SequenceResult()
    {
        if (cutSceneFinished) return; // Prevent Playback
        isAnimFinished = true;
        ScenarioSequence();
    }
    void OnDestroy()
    {
        startAnimSequence.Kill();
    }
}
