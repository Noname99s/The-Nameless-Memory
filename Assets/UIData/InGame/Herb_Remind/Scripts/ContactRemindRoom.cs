using DG.Tweening;
using Orpul1;
using UnityEngine;

public class ContactRemindRoom : MonoBehaviour, IInteractAble
{
    [SerializeField] DOTweenAnimation showInteractKey;
    bool isInContact = false;

    public void Interact()
    {
        Managers.Game.ChangeState(GameState.RemindRoom);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isInContact)
        {
            isInContact = true;
            if (showInteractKey != null)
            {
                showInteractKey.DORestart();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isInContact)
        {
            isInContact = false;
            if (showInteractKey != null)
            {
                showInteractKey.DOPlayBackwards();
            }
        }
    }
}
