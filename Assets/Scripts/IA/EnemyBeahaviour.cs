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
    }


    void Update()
    {

        if (!morreu) { 
            //distance to player
            float distToPlayer = Vector2.Distance(transform.position, player.position);

            if (distToPlayer <= attackRange)
            {
                //distance to attack is true
                isAttacking = true;
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
            //Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("player"));

        }
        
    }

     void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left side of the player, so move right

            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        } else if(transform.position.x > player.position.x)
        {
            // move left, right side

            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        if(lives >0)
            ChangeAnimationState(MINOTAURO_WALK);
    }

    void StopChasing()
    {
        rb2d.velocity = Vector2.zero; // new Vector2(0, 0);
        if (!isAttacking && lives >0)
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
            ChangeAnimationState(MINOTAURO_ATTACK);
        Invoke("AttackComplete", attackDelay);

    }

    private void OnParticleCollision(GameObject other)
    {
        lives--;


        if (lives == 0)
        {
            morreu = true;
            ChangeAnimationState(MINOTAURO_DEATH);
            Morreu();
            
            
        }
    }

    void Morreu ()
    {
        if (morreu)
        {
            Destroy(gameObject, 1f);
            Instantiate(DeathPlat, transform.position, Quaternion.identity);
        }
    }

  
}
