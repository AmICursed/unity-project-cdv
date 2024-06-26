using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class PlayerContoller : MonoBehaviour
{
 public float walkSpeed = 5f;
 public float runSpeed = 8f;
 public float airWalkSpeed = 3f;
 public float jumpImpulse = 10f;
 Damageable damageable;
TouchingDirection touchingDirections;
 public float CurrentMoveSpeed { get
 {
        if(CanMove)
        {
            if(IsMoving && !touchingDirections.IsOnWall){
                if(touchingDirections.IsGrounded){
             if(IsRunning)
                {
                    return runSpeed;
                }else{
                    return walkSpeed;
                }
                }
        else 
        {
            return airWalkSpeed;
        }
                }
        else{
            return 0;
    }
        } else{
            return 0;
        }
 }
 }
    Vector2 moveInput;

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving { 
    get{
        return _isMoving;
    } 
     set
    {
        _isMoving = value;
        animator.SetBool(AnimationStrings.isMoving, value);
    } }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning{
        get{
            return _isRunning;
        }
        set{
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    public bool _isFacingRight = true;
    public bool IsFacingRight { get{return _isFacingRight; } private set{
        if (_isFacingRight != value){
            transform.localScale *= new Vector2(-1, 1);
        }
        _isFacingRight = value;

    } }

    Rigidbody2D rb;
    Animator animator;

    public bool CanMove {get 
    {
        return animator.GetBool(AnimationStrings.canMove);
    }}

    public bool IsAlive {
        get{
            return animator.GetBool(AnimationStrings.isAlive);

        }
    }



    private void Awake() 
    {
        rb =  GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirection>();
        damageable = GetComponent<Damageable>();
    }
    private void FixedUpdate() 
    {
        if(!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if(rb.position.y < -15f)
        {
             SceneManager.LoadScene("GameplayScene");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(IsAlive){
             IsMoving = moveInput != Vector2.zero; 

             SeteFacingDirection(moveInput);
        } 
        else
        {
            IsMoving = false;
        }
    
    }

    private void SeteFacingDirection(Vector2 moveInput)
    {
       if(moveInput.x > 0 && !IsFacingRight){
        IsFacingRight = true;
       }else if(moveInput.x < 0 && IsFacingRight){
        IsFacingRight = false;
    }
    }
    public void OnRun(InputAction.CallbackContext context){
        if (context.started){
            IsRunning = true;
        }else if(context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext  context){
        if(context.started && touchingDirections.IsGrounded && CanMove){
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
}

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

   public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.rangeAttackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

}