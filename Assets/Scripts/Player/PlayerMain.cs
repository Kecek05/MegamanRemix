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
    [SerializeField] private LayerMask graveLayer;
    [SerializeField] private LayerMask graveJumpLayer;
    [SerializeField] private LayerMask graveBatLayer;
    [SerializeField] private float graveJumpMultplier;
    private float dirX = 0f;
    public PauseMenu pauseMenu;



    public ParticleSystem fire;

    public GrabController grabControll;

    //SFX
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource shootSound;

    //Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_WALK = "Player_Walk";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_ATTACK = "Player_Shoot";
    const string PLAYER_DEATH = "Player_Death";
    const string PLAYER_FALL = "Player_Fall";
    const string PLAYER_ATTACKWALK = "Player_AttackWalk";
    const string PLAYER_GRAB = "Player_Grab";
    const string PLAYER_GRABWALK = "Player_GrabWalk";
    private string currentState;

    private bool attackWalk = false;
    private bool isJumpPressed = false;
    private bool isAttackPressed = false;
    private bool isAttacking = false;
    public bool morto = false;
    [SerializeField] private float attackDelay = 1.5f;

   
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


            damageable.Damage(1);
            Morri();
            
        }
             Itouchable touchable = collision.gameObject.GetComponent<Itouchable>();
        if (touchable != null)
        {
            touchable.touch();
        }
       // print(collision);
        //print(touchable);
    }

    public void Morri()
    {
        morto = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        ChangeAnimationState(PLAYER_DEATH);
        if (!deathSound.isPlaying)
        {
            deathSound.Play();
        }

        Invoke("RestartGame", 2.5f);
    }
    void Update()
    {

        string nomeDaCena = SceneManager.GetActiveScene().name;
        if (nomeDaCena == "GameOver" || nomeDaCena == "Menu")
        {
            Cursor.visible = true;
        } else { 
            if (pauseMenu.GameIsPause)
            {
                Cursor.visible = true;
            } else
            {
                Cursor.visible = false;
            }
        }
        dirX = Input.GetAxisRaw("Horizontal");
        if ( morto == false)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            
        }
            

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            isJumpPressed = true;
        }

        if (Input.GetButtonDown("Fire1") && !morto && !grabControll.grabing)
        {
            isAttackPressed = true;
            if (dirX != 0)
                attackWalk = true;
        }


        UpdateAnimation();
       
    }
    //FIxedUpdate chamada a cada 0.04 segundos, usada com a fisica que tambem é sincronizada com o relogio
    public void FixedUpdate()
    {

        if (isJumpPressed && isGrounded() && morto == false)
        {
            if (isGraveJump())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * graveJumpMultplier);
            } else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            
            
            isJumpPressed = false;
            if (grabControll.grabing == false)
                ChangeAnimationState(PLAYER_JUMP);

        }

        if (isGrounded() && !isAttacking && morto == false)
        {
            if (dirX != 0 && !attackWalk && !grabControll.grabing)
            {
                ChangeAnimationState(PLAYER_WALK);
               
            } else if (dirX != 0 && !attackWalk && grabControll.grabing == true )
            {
                ChangeAnimationState(PLAYER_GRABWALK);
            } else if(grabControll.grabing == true)
            {
                ChangeAnimationState(PLAYER_GRAB);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        if (!isAttacking && morto == false)
        {
 
             if (dirX != 0 && !attackWalk && grabControll.grabing == true)
            {
                ChangeAnimationState(PLAYER_GRABWALK);
            }
            else if (grabControll.grabing == true)
            {
                ChangeAnimationState(PLAYER_GRAB);
            }
        }
        if (isAttackPressed)
        {
            isAttackPressed = false;
            if (!isAttacking)
            {
                isAttacking = true;
                Fire();
                if(dirX == 0)
                    ChangeAnimationState(PLAYER_ATTACK);
                else
                {
                    ChangeAnimationState(PLAYER_ATTACKWALK);
                    
                }
                    

                Invoke("AttackComplete", attackDelay);
            }
            //attackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
        }
        
    }


    void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void AttackComplete()
    {
        isAttacking = false;
        attackWalk = false;
    }
    private void Fire()
    {
        fire.Emit(1);
        shootSound.Play();
    }

    private bool isGrounded()
    {

        if (isChao() || isGrave() || isGraveJump() || isGraveBat())
        {
            return true;
        } else
        {
            return false;
        }
       

    }

    private bool isGraveBat()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, graveBatLayer);
    }
    private bool isGraveJump()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, graveJumpLayer);
    }
   private bool isChao()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private bool isGrave()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, graveLayer);
    }

    private void UpdateAnimation()
    {
        if (!morto)
        {
            // MovementState state;
            
                if (dirX > 0f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (dirX < 0f)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            
            if (rb.velocity.y > .1f  && grabControll.grabing == false)
            {
                ChangeAnimationState(PLAYER_JUMP);
            }
            else if (rb.velocity.y < -.1f && grabControll.grabing == false)
            {
                ChangeAnimationState(PLAYER_FALL);
            }
        
        }
    }


}

