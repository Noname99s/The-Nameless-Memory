using Orpul1;
using Orpul1.Player.Interact;
using Orpul1.State.Attack;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputListner : MonoBehaviour, IProcessAble
{
    [SerializeField] PlayerContext playerData;
    [SerializeField] PlayerInteractController playerInteractController;
    IInteractAble playerInteract;
    bool canInput = true;
    private void Awake()
    {
        playerInteract = playerInteractController;
        canInput = true;
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (!canInput) return;
        if (context.canceled)
        {
            playerData.moveInfo.moveDir = Vector2.zero;
            return;
        }
        playerData.moveInfo.moveDir = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!canInput) return;
        if (context.canceled)
        {
            playerData.jumpInfo.isPressJump = false;
            playerData.fallInfo.isFalling = true;
            if(playerData.jumpInfo.jumpCount>0)
                playerData.jumpInfo.jumpCount--;
        }
        else
        {
            if (playerData.jumpInfo.jumpCount <= 0)
                return;
            playerData.jumpInfo.jumpState = EJumpState.JumpReady;
            playerData.jumpInfo.isPressJump = true;
        }
    }
    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (!canInput) return;

        if (playerData.attackInfo.attackType == EPlayerAttack.StrongAttack) return;
        playerData.attackInfo.isPressAttack = !context.canceled;
        playerData.attackInfo.attackType = EPlayerAttack.NormalAttack;
    }

    public void StrongAttack(InputAction.CallbackContext context)
    {
        if (!canInput) return;

        if (playerData.attackInfo.attackType == EPlayerAttack.NormalAttack) return;
        playerData.attackInfo.isPressAttack = !context.canceled;
        playerData.attackInfo.attackType = EPlayerAttack.StrongAttack;
    }
    public void WallClimb(InputAction.CallbackContext context)
    {
        if (!canInput) return;
        playerData.moveInfo.isPressWallClimb = !context.canceled;
        playerData.moveInfo.isPressClimbLadder = !context.canceled;
        
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (!canInput) return;
        if (!context.canceled)
            playerInteract.Interact();
    }
    public void Skill(InputAction.CallbackContext context)
    {

    }

    public void Process()
    {
        canInput = false;
        playerData.SetIdleState();
    }

    public void Abort()
    {
        canInput = true;
    }
}
