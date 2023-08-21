using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

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


    //public Transform shootingPoint;
    //public GameObject bulletPrefab;

    public ParticleSystem fire;
    private enum MovementState { idle, running, jumping, falling}
      private MovementState state = MovementState.idle;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rd = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


  
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(1);
            
        }
             Itouchable touchable = collision.gameObject.GetComponent<Itouchable>();
        if (touchable != null)
        {
            touchable.touch();
        }
        print(collision);
        print(touchable);
    }

    void Update()
    {
        
        Cursor.visible = false;
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }


        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }




        UpdateAnimation();
    }

    private void Fire()
    {
        fire.Emit(1);
        // Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    private bool isGrounded()
    {

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

    }

 
    private void UpdateAnimation()
    {

       // MovementState state;
      
        if (dirX > 0f)
        {
            //  state = MovementState.running;

            
            transform.rotation = Quaternion.Euler(0, 0, 0);



        }
        else if (dirX < 0f)
        {
            //state = MovementState.running;
            
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
        else
        {
            
            //state = MovementState.idle;
        }

        //print(dirX);


        if (rb.velocity.y > .1f)
        {
           // state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            //state = MovementState.falling;
        }

        //anim.SetInteger("state", (int)state);

    }

   
}

