using System;
using Orpul1.Player;
using Orpul1.State;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputListner : MonoBehaviour
{
    [SerializeField] PlayerContext playerData;

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            playerData.moveDir = Vector2.zero;
            return;
        }
        playerData.moveDir = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            playerData.isPressJump = false;
            playerData.jumpState = EJumpState.JumpEnd;
            playerData.isFalling = true;
        }
        else
        {
            playerData.isPressJump = true;
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {
    }
    public void Skill(InputAction.CallbackContext context)
    {

    }
}
