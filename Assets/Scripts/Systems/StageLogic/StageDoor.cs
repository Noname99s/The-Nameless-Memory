using Orpul1;
using UnityEngine;

public class StageDoor : MonoBehaviour, IInteractAble
{
    [SerializeField] bool isNextStage = true; // true for next stage, false for previous stage


    public void Interact()
    {
        Managers.UI.GetPopupUI<InGameComposition>().OnFadeInSequence(() =>
        {
            Managers.Map.EntranceMap(isNextStage);
        });
    }
}
