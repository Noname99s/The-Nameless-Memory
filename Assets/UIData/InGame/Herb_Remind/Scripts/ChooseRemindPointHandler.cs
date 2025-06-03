using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseRemindPointHandler : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] Image slicedImage;
    [SerializeField] GameObject originalImage;
    DOTweenAnimation panelAnimation;
    Sequence panelSequence;
    Vector3 initPanelPos;
    void Awake()
    {
        if (panelSequence == null)
        {
            BuildSequence();
        }
    }
    void BuildSequence()
    { 
        initPanelPos = originalImage.transform.localPosition;
        panelAnimation = originalImage.GetComponent<DOTweenAnimation>();
        panelSequence = DOTween.Sequence()
        .Append(originalImage.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack))
        .SetAutoKill(false)
        .Pause();
        panelSequence.onComplete = () =>
        {
            panelAnimation.DORestart();
            originalImage.transform.localPosition = initPanelPos;
        };
    }
    void StopSequence()
    {
        Color color = slicedImage.color;
        color.a = 1f;
        slicedImage.color = color;
        if (panelSequence != null)
        {
            panelSequence.Pause();
            panelAnimation.DOPause();
            originalImage.transform.localScale = Vector3.one;
            originalImage.transform.localPosition = initPanelPos;
        }
        originalImage.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered Remind Point Handler");
        Color color = slicedImage.color;
        color.a = 0f;
        slicedImage.color = color;
        originalImage.SetActive(true);
        panelSequence.Restart();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited Remind Point Handler");
        StopSequence();
    }
}
