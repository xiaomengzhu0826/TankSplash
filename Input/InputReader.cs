using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
  
    public Vector2 MoveInput { get; private set; }

    public event Action FireEvent;

    public event Action FireCancelEvent;

    public event Action StartEvent;

    public event Action ResumeEvent;
    private void Start()
    {
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
     
    }

    public void OnFire(InputAction.CallbackContext context)
    {
  
        if (context.performed)
        {
            FireEvent?.Invoke();
           
        }
        if (context.canceled)
        {
            FireCancelEvent?.Invoke();
            
        }
    }

    public void OnStart(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            StartEvent?.Invoke();
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            ResumeEvent?.Invoke();
        }
    }

}
