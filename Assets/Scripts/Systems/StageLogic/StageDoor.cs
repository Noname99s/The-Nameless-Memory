using Orpul1;
using System.Collections;
using UnityEngine;

public class StageDoor : MonoBehaviour, IInteractAble
{
    [SerializeField] bool isNextStage = true; // true for next stage, false for previous stage

    //bool isInDoor = false;
    public void Interact()
    {
        Managers.Map.StopMove();
        Managers.UI.GetPopupUI<InGameComposition>().OnFadeInSequence(() =>
        {
            Managers.Map.TransitionMap(isNextStage);
        });
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        isInDoor = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        isInDoor = false;
    //    }
    //}
    
}
