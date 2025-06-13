using DG.Tweening;
using Orpul1.Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseRemindPointHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Stage stageType = Stage.Hub; // Default to Hub stage
    [SerializeField] RemindCutScene remindCutScene; // Reference to the RemindCutScene script
    [SerializeField]
    GameObject[] remindPanels = new GameObject[2]; // 0: Locked, 1: Unlocked

    [SerializeField] Image slicedImage;
    [SerializeField] GameObject originalImage;
    DOTweenAnimation panelAnimation;
    Sequence panelSequence;

    Vector3 initPanelPos;
    bool isUnlocked = false;
    void Awake()
    {
        isUnlocked = Managers.Game.playData.stageCleared[(int)stageType];
        remindPanels[0].SetActive(!isUnlocked); // Locked panel
        remindPanels[1].SetActive(isUnlocked); // Unlocked panel
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
        if (!isUnlocked) return; // Do nothing if the stage is locked
        Debug.Log("Pointer entered Remind Point Handler");
        Color color = slicedImage.color;
        color.a = 0f;
        slicedImage.color = color;
        originalImage.SetActive(true);
        panelSequence.Restart();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUnlocked) return; // Do nothing if the stage is locked
        Debug.Log("Pointer exited Remind Point Handler");
        StopSequence();
    }
    public void OnClickRemindPoint()
    {
        if (!isUnlocked) return; // Do nothing if the stage is locked
        panelSequence.Rewind();
        panelAnimation.DORewind();
        remindCutScene.OnRestartSequnece(); // Trigger the cutscene
        Managers.Sound.StopBGM();
        Managers.Sound.PlaySound2D(ESoundCategory.REMAINROOM, (int)stageType, EMixerGroup.BGM, true);
    }
}
