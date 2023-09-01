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

    [SerializeField] GameObject DeathPlat;
    Rigidbody2D rb2d;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource attackSound;

    //Retirar colisao com o player quando o inimigo morre
    public string layerToIgnore = "player";


    public float velocidadeQueda = 5.0f;

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

        if (!morreu)
        {
            //distance to player
            //float distToPlayer = Vector2.Distance(transform.position, player.position);
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

        if (transform.position.x == player.position.x && !isAttacking)
        {
            print(" é para atacar");
            AttackPlayer();
            return;
        }

        if (lives > 0 && isAttacking == false)
            ChangeAnimationState(MINOTAURO_WALK);

        print("andando");
    }

    void StopChasing()
    {
        //new Vector2(0, 0); bug na gravidade
        if (lives > 0)
            ChangeAnimationState(MINOTAURO_IDLE);
    }

    void AttackPlayer()
    {
        if (lives > 0)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                ChangeAnimationState(MINOTAURO_ATTACK);
                transform.Translate(Vector3.down * velocidadeQueda * Time.deltaTime);
                if (!attackSound.isPlaying)
                {
                    attackSound.Play();
                }
                Invoke("AttackComplete", attackDelay);
            }

            print("atacando");

            //if (transform.position.x < player.position.x)
            //{
            //    transform.localScale = new Vector2(1, 1);
            //}
            //else
            //{
            //    transform.localScale = new Vector2(-1, 1);
            //}
            //if (lives > 0)
            //{
            //    if (!isAttacking)
            //    {
            //        isAttacking = true;
            //        ChangeAnimationState(MINOTAURO_ATTACK);
            //        if (!attackSound.isPlaying)
            //        {
            //            attackSound.Play();
            //        }
            //        Invoke("AttackComplete", attackDelay);
            //    }


            //}
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

            ChangeAnimationState(MINOTAURO_DEATH);
            Morreu();


        }


    }

    void Morreu()
    {

        //Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("player"));
        this.GetComponent<PolygonCollider2D>().enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject, 1f);
        Instantiate(DeathPlat, transform.position, Quaternion.identity);

    }


}
