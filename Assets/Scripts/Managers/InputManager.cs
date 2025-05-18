using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    Action keyInput;
    public void OnMove(InputValue value)
    {
        Vector2 movePosi = value.Get<Vector2>();
    }
}
