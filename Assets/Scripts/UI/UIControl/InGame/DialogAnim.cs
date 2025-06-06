using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogAnim : MonoBehaviour , ISequenceBuilder,ISequnceResulter
{
    [Header("Fade Animation")]
    Sequence fadeAnimSequence;
    Transform initRootPos;
    [SerializeField] GameObject moveRoot;
    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] float stayDuration = 2f;
    public TextMeshProUGUI dialogText;
    [SerializeField] Image[] fadeAnimImg;

    [Header("Text Data")]
    int _currentTextIndex = 0;
    bool _isDialogActive = false;
    public List<string> _textDatas = new List<string>();
    private void Awake()
    {
        if (fadeAnimSequence == null)
        {
            BuildSequence();
        }
    }
    private void OnDisable()
    {
        if (fadeAnimSequence != null)
        {
            fadeAnimSequence.Pause();
        }
    }
    public void PlayAnim()
    {
        if (fadeAnimSequence != null)
        {
            _currentTextIndex = 0;
            RewindData();
            fadeAnimSequence.Restart();
        }
    }
    void RewindData()
    {
        moveRoot.transform.localPosition = initRootPos.localPosition;
        dialogText.color = new Color(dialogText.color.r, dialogText.color.g, dialogText.color.b, 0);
        for (int i = 0; i < fadeAnimImg.Length; i++)
        {
            fadeAnimImg[i].color = new Color(fadeAnimImg[i].color.r, fadeAnimImg[i].color.g, fadeAnimImg[i].color.b, 0);
        }
    }
    public void BuildSequence()
    {
        initRootPos = moveRoot.transform;

        fadeAnimSequence = DOTween.Sequence();
        fadeAnimSequence
            .Append(moveRoot.transform.DOLocalMove(Vector3.zero, fadeDuration).SetEase(Ease.OutQuad))
            .Join(dialogText.DOFade(1, fadeDuration).SetEase(Ease.OutQuad));
        for (int i = 0; i < fadeAnimImg.Length; i++)
        {
            fadeAnimSequence.Join(fadeAnimImg[i].DOFade(1, fadeDuration).SetEase(Ease.OutQuad));
        }

        fadeAnimSequence
            .AppendInterval(stayDuration)
            .Append(moveRoot.transform.DOMove(initRootPos.position, fadeDuration).SetEase(Ease.InQuad))
            .Join(dialogText.DOFade(0, fadeDuration).SetEase(Ease.InQuad));
        for (int i = 0; i < fadeAnimImg.Length; i++)
        {
            fadeAnimSequence.Join(fadeAnimImg[i].DOFade(0, fadeDuration).SetEase(Ease.InQuad));
        }
        fadeAnimSequence
            .OnStart(() =>
            {
                if (_textDatas.Count > 0)
                {
                    dialogText.text = _textDatas[_currentTextIndex++];
                    Debug.Log("CurrenTextIndex: " + _currentTextIndex);
                    _isDialogActive = _currentTextIndex < _textDatas.Count;
                }
            })
            .OnRewind(() =>
            {
                if (_textDatas.Count > 0)
                {
                    dialogText.text = _textDatas[_currentTextIndex++];
                    Debug.Log("CurrenTextIndex2: " + _currentTextIndex);
                    _isDialogActive = _currentTextIndex < _textDatas.Count;
                }
            })
            .SetAutoKill(false)
            .OnComplete(() => SequenceResult())
            .Pause();
    }
    public void SequenceResult()
    { 
        if (_isDialogActive)
        {
            this.gameObject.SetActive(true);
            fadeAnimSequence.Restart();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
