using Orpul1.State;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Orpul1.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] StateController stateController;
        [SerializeField] PlayerContext playerContext;
        void Start()
        {
            TransitionState(EPlayerState.Idle);
        }

        void Update()
        {
            stateController.Process();
        }
        public void TransitionState(EPlayerState state)
        {
            stateController.TransitionState((int)state);
        }
        private void OnGround(Collider2D other)
        {
            if (!other.CompareTag(ETag.Ground.ToString())) return;
            if (playerContext.jumpState == EJumpState.JumpEnd) return;
            playerContext.jumpState = EJumpState.JumpEnd;
            TransitionState(EPlayerState.Idle);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnGround(other);
        }
    }

}