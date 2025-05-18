using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputListner : MonoBehaviour
{
    [SerializeField] PlayerContext playerContext;
    public void Move(InputAction.CallbackContext context)
    {
        playerContext.movePosi = context.ReadValue<Vector2>();

    }
    public void Attack(InputAction.CallbackContext context)
    {

    }
    public void Skill(InputAction.CallbackContext context)
    {

    }
}
