using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoBarrilBehaviour : MonoBehaviour, IDamageable
{
    public int lives = 4;
    private bool morreu = false;

    private Animator anim;
    private string currentState;
    const string ESQUELETO_IDLE = "Esqueleto_Idle";
    const string ESQUELETO_ATTACK = "Esqueleto_Attack";
    const string ESQUELETO_DEATH = "EsqueletoDeath";
    //const string ESQUELETO_RELOADING = "Esqueleto_Reloading";

    public GameObject player; // Referência ao jogador.
    public GameObject barrilPrefab; // Prefab do barril a ser lançado.
    public float throwForce = 10.0f; // Força do lançamento do barril.
    public float throwingInterval = 3.0f; // Intervalo de tempo entre os lançamentos.


    [SerializeField] float agroRangeX;
    [SerializeField] float agroRangeY;
    private bool isAttacking = false;

    [SerializeField] GameObject DeathPlat;
    Rigidbody2D rb2d;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource attackSound;

    public string layerToIgnore = "player";

    public float duracaoPiscada;
    private SpriteRenderer spriteRenderer;

    private Transform throwPoint;

    #pragma warning disable CS0414
   // private bool canThrow = false;
    #pragma warning restore CS0414


    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);
        currentState = newState;
    }


    void Start()
    {
        throwPoint = transform.Find("ThrowPoint"); // Ponto de onde o barril será lançado.
    

            spriteRenderer = GetComponent<SpriteRenderer>();
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Espinhos"));
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("portal"));
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"));
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("graveJUMP"));
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("graveBat"));
        
    }

    public void Damage(float damageAmount)
    {
        // Hit();

    }

    void Update()
    {
        if (!morreu)
        {

            // Lança o barril na direção do jogador.
            float distToPlayerX = Mathf.Abs(transform.position.x - player.transform.position.x);
            float distToPlayerY = Mathf.Abs(transform.position.y - player.transform.position.y);
            if (distToPlayerX <= agroRangeX && distToPlayerY <= agroRangeY)
            {
                // pode atacar
                AttackPlayer();
                
            } else
            {
                StopChasing();
            }

            
        }


    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {
            ChangeAnimationState(ESQUELETO_ATTACK);
            float animDelay = anim.GetCurrentAnimatorStateInfo(0).length - 0.2f;
            Invoke("ThrowBarrelAtPlayer", animDelay);
            isAttacking = true;
            // StartCoroutine(ThrowBarrelsContinuously());
            


        }

        
    }
    void StopChasing()
    {
        //new Vector2(0, 0); bug na gravidade
        if (lives > 0)
            ChangeAnimationState(ESQUELETO_IDLE);
    }

    //IEnumerator ThrowBarrelsContinuously()
    //{

    //    ThrowBarrelAtPlayer();

    //    // Calcula a direção para o jogador.
    //    // Vector2 directionToPlayer = player.transform.position - transform.position;

    //    // Espera pelo intervalo antes de lançar o próximo barril.
    //    //float animDelay = anim.GetCurrentAnimatorStateInfo(0).length;
    //    //print(animDelay);
    //    //yield return new WaitForSeconds(animDelay);
    //    //ChangeAnimationState(ESQUELETO_IDLE);
    //    yield return new WaitForSeconds(throwingInterval);
    //    isAttacking = false;
    //    yield break;

    //}

    void AttackComplete()
    {
        isAttacking = false;
        
    }

    void ThrowBarrelAtPlayer()
    {
       // Rigidbody2D barrilRigidbody = barrilPrefab.GetComponent<Rigidbody2D>();
        Instantiate(barrilPrefab, throwPoint.position, throwPoint.rotation);

        // Calcula a direção para o jogador.
        //  Vector2 direction = (player.transform.position - throwPoint.position).normalized;

        // Aplica a força para lançar o barril na direção do jogador.
        // barrilRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);
        ChangeAnimationState(ESQUELETO_IDLE);
        Invoke("AttackComplete", throwingInterval);

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

            ChangeAnimationState(ESQUELETO_DEATH);
            Morreu();    
        }   
    }

    void Morreu ()
    {

        //Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("player"));
        this.GetComponent<PolygonCollider2D>().enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject,1f);
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