using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuView : UIPopup
{
    [SerializeField] DOTweenAnimation towerBack;
    [SerializeField] DOTweenAnimation logoBack;
    //[SerializeField] Button[] buttons;

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            towerBack.DOPlay();
            logoBack.DOPlay();
        }
    }
}
