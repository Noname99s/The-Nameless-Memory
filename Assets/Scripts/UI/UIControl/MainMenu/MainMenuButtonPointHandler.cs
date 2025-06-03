using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonPointHandler : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI[] texts = new TextMeshProUGUI[4];
    public void OnPointerEnter(PointerEventData eventData)
    {
        for(int i = 1; i < texts.Length; i++)
        {
            texts[i].color = Color.gray;
        }
        texts[0].color = Color.white; 
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        texts[0].color = Color.gray;
    }

}
