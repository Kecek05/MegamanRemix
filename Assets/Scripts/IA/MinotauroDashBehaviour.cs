using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotauroDashBehaviour : MonoBehaviour, IDamageable
{
    
    public int lives = 3;
    private bool morreu = false;


    //Animacao
    private Animator anim;
    private string currentState;
    const string MINOTAURO_IDLE = "Minotauro_Idle";
    const string MINOTAURO_PREPARING = "Minotauro_Preparing";
    const string MINOTAURO_DASH = "Minotauro_Dash";
    const string MINOTAURO_DEATH = "Minotauro_Death";

    public float dashSpeed;
    public float duracaoPreparing;
    private bool isAttacking = false;
    [SerializeField] private float attackDelay = 0.3f;

    [SerializeField] Transform player;
    [SerializeField] float agroRangeX;
    [SerializeField] float agroRangeY;

    [SerializeField] float moveSpeed;

    [SerializeField] GameObject DeathPlat;
    Rigidbody2D rb2d;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource attackSound;

    //Retirar colisao com o player quando o inimigo morre
    public string layerToIgnore = "player";


    // Hit Feedback
    public float duracaoPiscada;
    private SpriteRenderer spriteRenderer;

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    public void Damage(float damageAmount)
    {
        //hit
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Espinhos"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("portal"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("graveJUMP"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("graveBat"));
    }

    void Update()
    {

        if (!morreu)
        {
            //distance to player
            //float distToPlayer = Vector2.Distance(transform.position, player.position);
            float distToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
            float distToPlayerY = Mathf.Abs(transform.position.y - player.position.y);

            if (distToPlayerX < agroRangeX && distToPlayerY < agroRangeY)
            {
                //chase player
                AttackPlayer();
            }
            else
            {
                //dont chase
                StopChasing();
            }

        }

    }




    void StopChasing()
    {
        //new Vector2(0, 0); bug na gravidade
        if (lives > 0 && !isAttacking)
            ChangeAnimationState(MINOTAURO_IDLE);
    }

    void AttackPlayer()
    {
        
        if (lives > 0)
        {
            
            
            if (!isAttacking)
            {
                isAttacking = true;

                if (!attackSound.isPlaying)
                {
                    attackSound.Play();
                }
                
                StartCoroutine(Attack());

            }


        }



    }



    private IEnumerator Attack()
    {
        if (!morreu) { 
            // preparar ataque
            ChangeAnimationState(MINOTAURO_PREPARING);
            Vector2 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0;
            if (transform.position.x < player.position.x)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }
            yield return new WaitForSeconds(duracaoPreparing);
            // atacar
            ChangeAnimationState(MINOTAURO_DASH);
            rb2d.velocity = directionToPlayer.normalized * dashSpeed;
            //retornar
            yield return new WaitForSeconds(attackDelay);
            ChangeAnimationState(MINOTAURO_IDLE);
            isAttacking = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        lives--;

        if (!hitSound.isPlaying)
        {
            hitSound.Play();
        }
        StartCoroutine(HitFeedback());
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
    private IEnumerator HitFeedback()
    {
        // Reduz a transparência
        spriteRenderer.color = new Color(1f, 0.6941177f, 0.6941177f, 1f);

        // Aguarda a duração do piscar
        yield return new WaitForSeconds(duracaoPiscada);

        // Restaura a transparência original do jogador
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
