using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuView : UIPopup
{
    [SerializeField] DOTweenAnimation[] dOTweenAnimations;

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            for (int i = 0; i < dOTweenAnimations.Length; i++)
            {
                if (dOTweenAnimations[i].isActiveAndEnabled)
                {
                    dOTweenAnimations[i].DOPlay();
                }
            }
        }
    }
}
