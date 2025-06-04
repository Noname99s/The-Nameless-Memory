using UnityEngine;

namespace Orpul1.Player.Interact
{
    public class PlayerInteractController : MonoBehaviour, IInteractAble
    {
        [SerializeField] LayerMask detectLayer;
        [SerializeField] float interactRadius;
        bool isPressInteractKey = false;
        public void Interact()
        {
            isPressInteractKey = true;
            Collider2D target = Physics2D.OverlapCircle(transform.position, interactRadius, detectLayer.value);
            if (target == null) return;
            DoInteract(target.GetComponent<IInteractAble>());
        }
        private void DoInteract(IInteractAble target)
        {
            if (target == null) return;
            target.Interact();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isPressInteractKey) return;
            InteractObj(other.GetComponent<IInteractAble>());
        }
        private void InteractObj(IInteractAble target)
        {
            if (target == null) return;
            target.Interact();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }
    }
}