using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerHorizontalSpeed = 7f;
    public float PlayerVerticalSpeed   = 7f;
    public bool flyHack = false;

    private Rigidbody2D    rb;
    private BoxCollider2D  coll;
    private SpriteRenderer sp;
    private Animator       animator;

    [SerializeField] private LayerMask jumpableGround;

    void Start()
    {
        rb       = GetComponent<Rigidbody2D>();
        coll     = GetComponent<BoxCollider2D>();
        sp       = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HorizontalMovement();
        VerticalMovement();

        
    }

    void FixedUpdate()
    {
        // Handle wall collisions
        HandleWallCollisions();
        rb.AddForce(Vector3.down * 6);

    }


    private void HorizontalMovement()
    {
        // Horizontal Movement
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * PlayerHorizontalSpeed, rb.velocity.y);

        // Sprite and Collider Flip
        if (dirX < 0)
        {
            sp.flipX = true;
            coll.offset = new Vector2(-Mathf.Abs(coll.offset.x), coll.offset.y);
        }
        else if (dirX > 0)
        {
            sp.flipX = false;
            coll.offset = new Vector2(Mathf.Abs(coll.offset.x), coll.offset.y);
        }

        // Set the isRunning parameter based on horizontal movement
        bool isCurrentlyRunning = Mathf.Abs(dirX) > 0;
        animator.SetBool("isRunning", isCurrentlyRunning);

        // Handle transitions when starting and stopping running
        if (isCurrentlyRunning && !animator.GetBool("isRunning"))
        {
            // Start running
            animator.SetBool("isRunning", true);
            animator.SetTrigger("StartRunning");
        }
        else if (!isCurrentlyRunning && animator.GetBool("isRunning"))
        {
            // Stop running
            animator.SetBool("isRunning", false);
            animator.SetTrigger("StopRunning");
        }



        // Check if the StartRunning trigger is active and the player is not moving

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle") &&
            animator.IsInTransition(0) && 
            animator.GetAnimatorTransitionInfo(0).IsName("AnyState -> Player_Start_Running"))
        {
            // Reset the StartRunning trigger to return to the idle animation
            animator.ResetTrigger("StartRunning");
        }
       
       
    }

    private void VerticalMovement()
    {
        // Vertical Movement
        if (!flyHack)
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, PlayerVerticalSpeed);
                PlayerVerticalSpeed += .005f;

                // Trigger the start running animation when jumping
                animator.SetTrigger("StartRunning");
            }
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, PlayerVerticalSpeed);
                PlayerVerticalSpeed += .005f;

                // Trigger the start running animation when jumping
                animator.SetTrigger("StartRunning");
            }
        }
    }




    private void HandleWallCollisions()
    {
        float raycastLength = 0.1f;
        float raycastOffset = 0.02f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(rb.velocity.x), raycastLength, jumpableGround);

        if (hit.collider != null)
        {
            // Adjust player position to prevent passing through walls
            float hitDistance = Mathf.Abs(hit.point.x - transform.position.x);
            float offset = raycastOffset + 0.01f; // Add a small offset to ensure separation from the wall
            if (rb.velocity.x > 0)
                transform.position = new Vector2(hit.point.x - hitDistance + offset, transform.position.y);
            else if (rb.velocity.x < 0)
                transform.position = new Vector2(hit.point.x + hitDistance - offset, transform.position.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .2f, jumpableGround);
    }

    // Animation Event method called at the end of Player_Start_Running animation
    public void OnPlayerStartRunningAnimationEnd()
    {
        // Trigger the transition to Player_Running
        animator.SetTrigger("StartRunningEnd");
    }
}
