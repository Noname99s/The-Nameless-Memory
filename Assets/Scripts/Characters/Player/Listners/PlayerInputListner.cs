using System;
using Orpul1.Player;
using Orpul1.State;
using Orpul1.State.Attack;
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
    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (playerData.attackContext.attackType == EPlayerAttack.StrongAttack) return;
        playerData.isPressAttack = !context.canceled;
        playerData.attackContext.attackType = EPlayerAttack.NormalAttack;
    }

    public void StrongAttack(InputAction.CallbackContext context)
    {
        if (playerData.attackContext.attackType == EPlayerAttack.NormalAttack) return;
        playerData.isPressAttack = !context.canceled;
        playerData.attackContext.attackType = EPlayerAttack.StrongAttack;
    }

    public void Skill(InputAction.CallbackContext context)
    {

    }
}
