using Orpul1;
using System.Collections;
using UnityEngine;

public class StageDoor : MonoBehaviour, IInteractAble
{
    [SerializeField] bool isNextStage = true; 

    public void Interact()
    {
        Managers.Map.StopMove();
        Managers.UI.GetPopupUI<InGameComposition>().OnFadeInSequence(() =>
        {
            Managers.Map.TransitionMap(isNextStage);
        });
    }
}
