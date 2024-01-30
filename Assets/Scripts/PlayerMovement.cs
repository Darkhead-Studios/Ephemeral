using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float PlayerHorizontalSpeed = 7f;
    public float PlayerVerticalSpeed = 6f;
    public bool flyHack = false;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sp;

    [SerializeField] private LayerMask jumpableGround;

    void Start()
    {
        rb   = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sp   = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        // Horizontal Movement
        float dirX  = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * PlayerHorizontalSpeed, rb.velocity.y);
        if (dirX < 0)      sp.flipX = true;  // Sprite Flip
        else if (dirX > 0) sp.flipX = false; // Sprite Flip

        // Vertical Movement
        if (!flyHack)
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
                rb.velocity = new Vector2(rb.velocity.x, PlayerVerticalSpeed);  
        } else {
            if (Input.GetButton("Jump"))
                rb.velocity = new Vector2(rb.velocity.x, PlayerVerticalSpeed);
        }
        

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .2f, jumpableGround);
    }

}
