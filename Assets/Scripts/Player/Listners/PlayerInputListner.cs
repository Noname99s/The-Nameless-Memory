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
            transitionTo.Invoke(EPlayerState.Idle);
            return;
        }
        playerContext.movePosi = context.ReadValue<Vector2>();
        transitionTo.Invoke(EPlayerState.Move);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(playerContext.jumpState == EJumpState.Jumping) return;
        
        playerContext.isJump = true;
        playerContext.jumpState = EJumpState.JumpStart;
        transitionTo.Invoke(EPlayerState.Jump);
    }
    public void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack");
    }
    public void Skill(InputAction.CallbackContext context)
    {

    }
}
