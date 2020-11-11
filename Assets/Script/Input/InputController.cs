using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    private Vector3 mousePosition;
    

   public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        GameManager.Instance.playercontroller.SetPositionInput(input);
        
    }
    
    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.action.ReadValue<Vector2>();

    }
    
    public Vector3 GetMousePosition()
    {
        if (mousePosition == null) return Vector3.zero;

        return mousePosition;
    }
    



}
