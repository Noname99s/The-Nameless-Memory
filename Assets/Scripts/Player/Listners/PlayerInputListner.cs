using System;
using Orpul1.Player;
using Orpul1.State;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputListner : MonoBehaviour
{
    [SerializeField] PlayerContext playerContext;
    [SerializeField] UnityEvent<EPlayerState> transitionTo;

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            playerContext.movePosi = Vector2.zero;
            transitionTo.Invoke(EPlayerState.Idle);
            return;
        }
        playerContext.movePosi = context.ReadValue<Vector2>();
        transitionTo.Invoke(EPlayerState.Move);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            playerContext.isPressJump = false;
        }
        else
        {
            playerContext.isPressJump = true;
            transitionTo.Invoke(EPlayerState.Move);
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {
    }
    public void Skill(InputAction.CallbackContext context)
    {

    }
}
