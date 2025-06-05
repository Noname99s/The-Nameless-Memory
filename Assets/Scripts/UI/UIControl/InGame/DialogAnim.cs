using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogAnim : MonoBehaviour
{
    [Header("Fade Animation")]
    Sequence fadeAnimSequence;
    Transform initRootPos;
    [SerializeField] GameObject moveRoot;
    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] float stayDuration = 2f;
    public TextMeshProUGUI dialogText;
    [SerializeField] Image[] fadeAnimImg;

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
        //fadeAnimSequence.Rewind();
    }
    void BuildSequence()
    {
        initRootPos = moveRoot.transform;

        fadeAnimSequence = DOTween.Sequence();
        fadeAnimSequence.Append(moveRoot.transform.DOLocalMove(Vector3.zero, fadeDuration).SetEase(Ease.OutQuad));
        fadeAnimSequence.Join(dialogText.DOFade(1, fadeDuration).SetEase(Ease.OutQuad));
        for (int i = 0; i < fadeAnimImg.Length; i++)
        {
            fadeAnimSequence.Join(fadeAnimImg[i].DOFade(1, fadeDuration).SetEase(Ease.OutQuad));
        }

        fadeAnimSequence.AppendInterval(stayDuration);

        fadeAnimSequence.Append(moveRoot.transform.DOMove(initRootPos.position, fadeDuration).SetEase(Ease.InQuad));
        fadeAnimSequence.Join(dialogText.DOFade(0, fadeDuration).SetEase(Ease.InQuad));
        for (int i = 0; i < fadeAnimImg.Length; i++)
        {
            fadeAnimSequence.Join(fadeAnimImg[i].DOFade(0, fadeDuration).SetEase(Ease.InQuad));
        }
        fadeAnimSequence.SetAutoKill(false);
        fadeAnimSequence.OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        fadeAnimSequence.Pause();
    }
}
