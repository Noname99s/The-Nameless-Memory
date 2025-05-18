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
            stateController.TransitionState((int)EPlayerState.Idle);
        }

        void Update()
        {
            stateController.Process();
        }
        public void Move(InputAction.CallbackContext context)
        {
            if(context.canceled)
            {
                stateController.TransitionState((int)EPlayerState.Idle);
                return;
            }
            playerContext.movePosi = context.ReadValue<Vector2>();
            stateController.TransitionState((int)EPlayerState.Move);
        }
        public void Attack(InputAction.CallbackContext context)
        {
            Debug.Log("Attack");
            //stateController.TransitionState((int)EPlayerState.Move);
        }
        public void Skill(InputAction.CallbackContext context)
        {

        }
    }

}