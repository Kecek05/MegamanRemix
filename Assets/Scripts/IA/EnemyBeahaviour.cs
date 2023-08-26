using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeahaviour : MonoBehaviour, IDamageable
{

    public int lives = 4;
    private bool morreu = false;
    //Animacao
    private Animator anim;
    private string currentState;
    const string MINOTAURO_IDLE = "Minotauro_Idle";
    const string MINOTAURO_WALK = "Minotauro_Walk";
    const string MINOTAURO_ATTACK = "Minotauro_Att";
    const string MINOTAURO_DEATH = "Minotauro_Death";
    private bool isAttacking = false;
    [SerializeField] private float attackDelay = 0.3f;

    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float attackRange;

    [SerializeField] float moveSpeed;

    [SerializeField] GameObject DeathPlat;
    Rigidbody2D rb2d;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource attackSound;

    //Retirar colisao com o player quando o inimigo morre
    public string layerToIgnore = "player";

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }
    public void Damage(float damageAmount)
    {
       // Hit();

    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Espinhos"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("portal"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("graveJUMP"));
    }


    void Update()
    {

        if (!morreu) { 
            //distance to player
            float distToPlayer = Vector2.Distance(transform.position, player.position);

            if (distToPlayer <= attackRange)
            {
                //distance to attack is true
                
                AttackPlayer();

            }
            else if (distToPlayer < agroRange)
            {
                //chase player
                ChasePlayer();
            }
            else
            {
                //dont chase
                StopChasing();
            }
       
        } else
        {
            

        }
        
    }

     void ChasePlayer()
    {
        
            if (transform.position.x < player.position.x &&  isAttacking == false)
            {
                //enemy is to the left side of the player, so move right

                rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
            else if (transform.position.x > player.position.x && isAttacking == false)
            {
                // move left, right side

                rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
       
        
        if(lives >0 && isAttacking == false)
            ChangeAnimationState(MINOTAURO_WALK);
    }

    void StopChasing()
    {
        //new Vector2(0, 0); bug na gravidade
        if (lives >0)
            ChangeAnimationState(MINOTAURO_IDLE);
    }

    void AttackPlayer()
    {
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        } else
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (lives > 0)
        {
            if(!isAttacking)
            {
                isAttacking = true;
                ChangeAnimationState(MINOTAURO_ATTACK);
                attackSound.Play();
                Invoke("AttackComplete", attackDelay);
            }
            
            
        }
            
       

    }

    private void OnParticleCollision(GameObject other)
    {
        lives--;

        hitSound.Play();
        if (lives <= 0)
        {
            
            morreu = true;

            ChangeAnimationState(MINOTAURO_DEATH);
            Morreu();
            
            
        }

        
    }

    void Morreu ()
    {

        //Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("player"));
        this.GetComponent<PolygonCollider2D>().enabled = false;
        Destroy(gameObject,1f);
        Instantiate(DeathPlat, transform.position, Quaternion.identity);
        
    }

  
}
