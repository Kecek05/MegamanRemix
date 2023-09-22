using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour, IDamageable
{

    public int lives = 2;
    private bool morreu = false;
    //Animacao
    private Animator anim;
    private string currentState;
    const string MINOTAURO_IDLE = "Minotauro_Idle";
    const string MINOTAURO_WALK = "Bat_Walk";
    const string MINOTAURO_ATTACK = "Bat_Down";
    const string MINOTAURO_DEATH = "Bat_Death";
    private bool isAttacking = false;
    [SerializeField] private float attackDelay = 0.3f;

    [SerializeField] Transform player;
    [SerializeField] float agroRangeX;
    [SerializeField] float agroRangeY;
    [SerializeField] float attackRangeX;
    [SerializeField] float attackRangeY;

    [SerializeField] float moveSpeed;
    [SerializeField] float velocidadeQueda;

    [SerializeField] GameObject DeathPlat;
    Rigidbody2D rb2d;

    [SerializeField] private AudioSource hitSound;


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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Morreu();
        }
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

        if (!morreu)
        {
            
            float distToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
            float distToPlayerY = Mathf.Abs(transform.position.y - player.position.y);

            if (distToPlayerX <= attackRangeX && distToPlayerY <= attackRangeY)
            {
                //distance to attack is true

                AttackPlayer();

            }
            else if (distToPlayerX < agroRangeX && distToPlayerY < agroRangeY)
            {
                //chase player
                ChasePlayer();
            }
            else
            {
                //dont chase
                StopChasing();
            }
         

        }
        else
        {


        }
        
    }

    void ChasePlayer()
    {

        if (transform.position.x < player.position.x && isAttacking == false)
        {


            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > player.position.x && isAttacking == false)
        {


            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        if (transform.position.x == player.position.x && !isAttacking)
        {
           
            AttackPlayer();
            return;
        }

        if (lives > 0 && isAttacking == false)
            ChangeAnimationState(MINOTAURO_WALK);

       
    }

    void StopChasing()
    {

        if (lives > 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            //ChangeAnimationState(MINOTAURO_IDLE);

        }
            
    }

    void AttackPlayer()
    {
        if (lives > 0)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                ChangeAnimationState(MINOTAURO_ATTACK);
                rb2d.velocity = new Vector2( 0, -velocidadeQueda);

                Invoke("AttackComplete", attackDelay);
            }

            

           
        }


    }

    private void OnParticleCollision(GameObject other)
    {
        lives--;

        if (!hitSound.isPlaying)
        {
            hitSound.Play();
        }
        if (lives <= 0)
        {

            morreu = true;

           
            Morreu();


        }


    }

    void Morreu()
    {
        ChangeAnimationState(MINOTAURO_DEATH);
        if (!hitSound.isPlaying)
        {
            hitSound.Play();
        }
        //Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("player"));
        this.GetComponent<PolygonCollider2D>().enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject, 1f);
        Instantiate(DeathPlat, transform.position, Quaternion.identity);

    }


}
