using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D),  typeof(TouchingDirection))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
     Rigidbody2D rb;
     TouchingDirection touchingDirections;
    public enum WalkableDirection {Right, Left};
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get {   return _walkDirection; }
        set {   
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }
            if (value == WalkableDirection.Right)
            {
                walkDirectionVector = Vector2.right;
            }else if (value == WalkableDirection.Left)
            {
                walkDirectionVector = Vector2.left;
            }
            _walkDirection = value; }
    }

     private void Awake() 
    {
        rb =  GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirection>();
    }
    private void FixedUpdate() 
    {

        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
       if(WalkDirection == WalkableDirection.Right){
        WalkDirection = WalkableDirection.Left;
       }else if (WalkDirection == WalkableDirection.Left)
       {
        WalkDirection = WalkableDirection.Right;
       }else
       {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
       }
    }

    void Start()
    {
        
    }

 
    void Update()
    {
        
    }
}
