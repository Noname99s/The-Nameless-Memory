using Orpul1.State;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Orpul1.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] StateController stateController;
        [SerializeField] PlayerContext playerContext;
        [SerializeField] float rayDistance = 1f;
        [SerializeField] Vector3 rayPosi = Vector3.zero;
        Ray2D ray = new Ray2D();
        RaycastHit2D rayHit = new RaycastHit2D();
        private void Awake()
        {
            ray.origin = transform.position;
        }
        void Start()
        {
            TransitionState(EPlayerState.Idle);
        }

        void Update()
        {
            OnGround();
            stateController.Process();
        }
        public void TransitionState(EPlayerState state)
        {
            stateController.TransitionState((int)state);
        }
        private void OnGround()
        {
            if (playerContext.jumpState == EJumpState.JumpReady) return;
            ray.origin = transform.position + rayPosi;
            ray.direction = Vector2.down;
            Debug.DrawRay(ray.origin, ray.direction * rayDistance,Color.green);
            rayHit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance);
            if (rayHit.collider == null) return;
            Debug.Log("Hit" + rayHit.collider.tag);

            if (!rayHit.collider.CompareTag(ETag.Floor.ToString())) return;
            
            playerContext.jumpState = EJumpState.JumpReady;
        }
    }

}