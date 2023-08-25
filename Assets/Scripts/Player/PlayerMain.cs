using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer rd;
    private Animator anim;

    //Audio
    //public GameObject audioManagerPrefab;
    //private AudioManager audioManager;
    


    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private LayerMask jumpableGround;
 

    private float dirX = 0f;



    public ParticleSystem fire;



    //Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_WALK = "Player_Walk";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_ATTACK = "Player_Shoot";
    const string PLAYER_DEATH = "Player_Death";
    const string PLAYER_FALL = "Player_Fall";
    private string currentState;

    private bool isJumpPressed = false;
    private bool isAttackPressed = false;
    private bool isAttacking = false;
    private bool morto = false;
    [SerializeField] private float attackDelay = 0.3f;


    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rd = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayBackgroundMusic();
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            
            
            //damageable.Damage(1);
            morto = true;
            ChangeAnimationState(PLAYER_DEATH);
            Invoke("RestartGame", 2f);
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
        if (!isAttacking && morto == false)
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }


        if (Input.GetButtonDown("Fire1") && dirX == 0 && isGrounded() && morto == false)
        {
            isAttackPressed = true;
            
        }

       
        UpdateAnimation();
    }

    public void FixedUpdate()
    {

        if (isJumpPressed && isGrounded() && morto == false)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);

        }

        if (isGrounded() && !isAttacking && morto == false)
        {
            if (dirX != 0)
            {
                ChangeAnimationState(PLAYER_WALK);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        if (isAttackPressed && isGrounded())
        {
            isAttackPressed = false;
            if (!isAttacking)
            {
                isAttacking = true;
                Fire();
                ChangeAnimationState(PLAYER_ATTACK);
            }
            attackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("AttackComplete", attackDelay);
            
        }
    }


    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void AttackComplete()
    {
        isAttacking = false;
    }
    private void Fire()
    {
        fire.Emit(1);
    }

    private bool isGrounded()
    {

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

    }

   

    private void UpdateAnimation()
    {
        if (!morto)
        {
            // MovementState state;
            if (!isAttacking)
            {
                if (dirX > 0f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (dirX < 0f)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            if (rb.velocity.y > .1f)
            {
                ChangeAnimationState(PLAYER_JUMP);
            }
            else if (rb.velocity.y < -.1f)
            {
                ChangeAnimationState(PLAYER_FALL);
            }
        }
    }


}

