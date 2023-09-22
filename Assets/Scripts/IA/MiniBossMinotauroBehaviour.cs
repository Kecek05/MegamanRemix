using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossMinotauroBehaviour : MonoBehaviour, IDamageable
{
    public int lives = 3;
    private bool morreu = false;
    private bool doDash = true, doFire = false, startBattle = false, doWalk = false;

    public HealthBarScript healthBar;

    [SerializeField] public GameObject Fire;
    //Animacao
    private Animator anim;
    private string currentState;
    const string MINOTAURO_IDLE = "MinoBoss_Idle";
    const string MINOTAURO_PREPARING = "MinoBoss_Preparando";
    const string MINOTAURO_DASH = "MinoBoss_Dash";
    const string MINOTAURO_DEATH = "MinoBoss_Death";
    const string MINOTAURO_FOGO = "MinoBoss_Fogo";
    const string MINOTAURO_WALK = "MinoBoss_Walk";

    public float dashSpeed;
    public float duracaoPreparing;
    private bool isAttacking = false;
    [SerializeField] private float attackDelay = 0.3f;
    [SerializeField] private float delayBetweenAtt;

    [SerializeField] Transform player;
    [SerializeField] float agroRangeX;
    [SerializeField] float agroRangeY;

    [SerializeField] float attackRangeX;
    [SerializeField] float attackRangeY;

    [SerializeField] float moveSpeed;

    [SerializeField] GameObject DeathPlat;
    Rigidbody2D rb2d;


    //AUDIO
    [SerializeField] private AudioSource FireSound;
    [SerializeField] private AudioSource DashSound;
    [SerializeField] private AudioSource DeathSound;

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

        healthBar.SetMaxHealth(lives);
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
                //comeca luta
                startBattle = true;
                healthBar.HealthToggle(true);
            }
            else
            {
                if(!startBattle)
                StopChasing();
            }

            if (startBattle)
            {
                if (doDash)
                {
                    if (!isAttacking)
                    {
                        print("tryDash");
                        isAttacking = true;
                        StartCoroutine(DashAttack());
                    }
                }
                else if (doWalk)
                {
                    Walk();
                }
                else if (doFire)
                {
                    print("doFire");


                    if (!isAttacking)
                    {
                        print("tryFire");
                        isAttacking = true;
                        StartCoroutine(FireAttack());
                    }
                }
            }
            


        } else
        {
            ChangeAnimationState(MINOTAURO_DEATH);
        }

    }

    private IEnumerator DashAttack()
    {
        print("DashAttack");
        if (!morreu)
        {
            if (!DashSound.isPlaying)
            {
                DashSound.Play();
            }
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

            float animDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animDelay);
            ChangeAnimationState(MINOTAURO_IDLE);
            yield return new WaitForSeconds(delayBetweenAtt - animDelay);
           
            isAttacking = false;
            doDash = false;
            doWalk = true;
            doFire = false;
        }
    }
    private void DoFire()
    {
        doFire = true;
    }

    private void Walk()
    {
        if(!morreu) { 
            print("doWalk");
            float distToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
            float distToPlayerY = Mathf.Abs(transform.position.y - player.position.y);

            if (distToPlayerX < attackRangeX && distToPlayerY < attackRangeY)
            {
                doWalk = false;
                Invoke("DoFire", 1.5f);
            } else
            {
                print("walking");
                if (transform.position.x < player.position.x)
                {
              

                    rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                    transform.localScale = new Vector2(1, 1);
                }
                else if (transform.position.x > player.position.x )
                {

                    rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                    transform.localScale = new Vector2(-1, 1);
                }

                if (lives > 0 && isAttacking == false)
                    ChangeAnimationState(MINOTAURO_WALK);
            }
        }
    }
    private IEnumerator FireAttack()
    {
        if (!morreu) {
            if (!FireSound.isPlaying)
            {
                FireSound.Play();
            }
            print("FireAttack");

            ChangeAnimationState(MINOTAURO_FOGO);
            float animDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            if (transform.position.x < player.position.x)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }
            Fire.SetActive(true);
            yield return new WaitForSeconds(animDelay -0.2f);



                yield return new WaitForSeconds(attackDelay);
                Fire.SetActive(false);
                ChangeAnimationState(MINOTAURO_IDLE);
                isAttacking = false;
                doFire = false;
                doDash = true;
        }
    }



    void StopChasing()
    {
        //new Vector2(0, 0); bug na gravidade
        if (lives > 0 && !isAttacking)
            ChangeAnimationState(MINOTAURO_IDLE);
    }


    private void OnParticleCollision(GameObject other)
    {
        lives--;
        healthBar.SetHealth(lives);
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
        healthBar.HealthToggle(false);
        if (!DeathSound.isPlaying)
        {
            DeathSound.Play();
        }
        //Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("player"));
        this.GetComponent<BoxCollider2D>().enabled = false;
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
