using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{
    public Animator anim;
    public Vector2 MovementInput { get; private set; }
    
    public bool AimInput { get; private set; }
    public bool AttackInput { get; private set; } = false;

    public bool JumpInput { get; private set; } = false;
   
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        //print("movin: " + MovementInput);
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.GamePaused)
        {
            AttackInput = true;
            print("atk " + AttackInput);
        }
    }
    public void UseAttackInput() { print("lol"); AttackInput = false; }

    public void OnAimInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AimInput = true;
            print("aim " + AimInput);
        }
        if (context.canceled)
        {
            AimInput = false;
            print("aim " + AimInput);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.GamePaused)
        {
            JumpInput = true;
            print("jump " + JumpInput);
        }
    }
    public void UseJumpInput() { anim.SetBool("jump", false); JumpInput = false; }


}
