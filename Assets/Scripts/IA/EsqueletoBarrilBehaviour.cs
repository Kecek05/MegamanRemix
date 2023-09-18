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
    const string ESQUELETO_DEATH = "Esqueleto_Death";
    const string ESQUELETO_RELOADING = "Esqueleto_Reloading";

    public GameObject player; // Referência ao jogador.
    public GameObject barrilPrefab; // Prefab do barril a ser lançado.
    public float throwForce = 10.0f; // Força do lançamento do barril.
    public float throwingInterval = 3.0f; // Intervalo de tempo entre os lançamentos.

    [SerializeField] float agroRangeX;
    [SerializeField] float agroRangeY;

    [SerializeField] GameObject DeathPlat;
    Rigidbody2D rb2d;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource attackSound;

    public string layerToIgnore = "player";

    public float duracaoPiscada;
    private SpriteRenderer spriteRenderer;

    private Transform throwPoint;

    #pragma warning disable CS0414
    private bool canThrow = false;
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
        StartCoroutine(ThrowBarrelsContinuously());

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
     ///
    }

    IEnumerator ThrowBarrelsContinuously()
    {
        while (true)
        {
            if (player == null || barrilPrefab == null || throwPoint == null)
                yield break;

            // Lança o barril na direção do jogador.
            ThrowBarrelAtPlayer();
            
              // Calcula a direção para o jogador.
            Vector2 directionToPlayer = player.transform.position - transform.position;

            // Espera pelo intervalo antes de lançar o próximo barril.
            yield return new WaitForSeconds(throwingInterval);
        }
    }

    void ThrowBarrelAtPlayer()
    {
        GameObject barril = Instantiate(barrilPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D barrilRigidbody = barril.GetComponent<Rigidbody2D>();

        if (barrilRigidbody!= null)
        {
            // Calcula a direção para o jogador.
            Vector2 direction = (player.transform.position - throwPoint.position).normalized;

            // Aplica a força para lançar o barril na direção do jogador.
            barrilRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);
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