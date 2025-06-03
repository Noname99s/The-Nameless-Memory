using DG.Tweening;
using UnityEngine;

public class ContactRemindRoom : MonoBehaviour
{
    [SerializeField] DOTweenAnimation showInteractKey;
    bool isInContact = false;
    public void Update()
    {
        if (isInContact)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Managers.Game.ChangeState(GameState.RemindRoom);
            }
        }
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
