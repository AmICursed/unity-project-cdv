using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingEye : MonoBehaviour
{

    public float flightSpeed = 2f;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;
    public float waypointReachedDistance = 0.1f;
    Animator animator;
    Rigidbody2D rb;
    Transform nextWaypoint;
    Damageable damageable;
    public Collider2D deathCollider;
    int waypointNum = 0;
    public bool _hasTarget = false;
     public bool HasTarget 
     {
        get{ return _hasTarget;} 
        private set{
           _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
     }}

        public bool CanMove{
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }


    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start(){
        nextWaypoint = waypoints[waypointNum];
    }

    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate() {
        if(damageable.IsAlive){
            if(CanMove){
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight(){
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();
        
        if(distance <= waypointReachedDistance)
        {
            waypointNum++;


            if(waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

        private void UpdateDirection(){

                Vector3 locScale = transform.localScale;

            if(transform.localScale.x > 0)
            {
                    if(rb.velocity.x < 0)
                    {
                    transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
                    }
            }else
            {
                 if(rb.velocity.x > 0)
                    {
                    transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
                    }
            }
        }


}
