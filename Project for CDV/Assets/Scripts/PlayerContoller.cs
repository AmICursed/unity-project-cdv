using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerContoller : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkSpeed = 5f;
    Vector2 moveInput;
    public bool IsMoving { get; private set; }


    private void Awake() 
    {
        rb =  GetComponent<Rigidbody2D>();    
    }
    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
        IsMoving = moveInput != Vector2.zero; 
    }

    void OnAttack()
    {

    }
}
