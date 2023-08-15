using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer rd;
    private Animator anim;

  

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
  

    private enum MovementState { idle, running, jumping, falling}
    private MovementState state = MovementState.idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rd = GetComponent <SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
       
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            gameObject.tag = "inv";
            Color newColor = new Color(Random.value, Random.value, Random.value);
            rd.material.color = newColor;
            

            // Execute qualquer ação que você desejar aqui
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameObject.tag = "Player";
            rd.material.color = Color.white;

        }
        UpdateAnimation();
    }


    private bool isGrounded()
    {

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        
    }
    private void UpdateAnimation()
    {

        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            rd.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            rd.flipX = true;
        } else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        } else if ( rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
        
    }

}
