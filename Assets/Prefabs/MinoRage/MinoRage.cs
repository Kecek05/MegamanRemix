using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinoRage : MonoBehaviour , IDamageable
{
    public GameObject player; // Refer�ncia ao jogador.
    public float dashSpeed = 10.0f; // Velocidade do ataque de dash.
    public float dashCooldown = 2.0f; // Tempo de recarga do ataque de dash.
    public Animator animator;

    private bool isDashing = false;
    private float dashTimer = 0.0f;

    
    void Start()
    {
        // Obt�m a refer�ncia ao componente Animator.
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (player == null)
            return;

        // Verifica se o jogador est� dentro do alcance de vis�o da IA.
        Vector2 directionToPlayer = player.transform.position - transform.position;
        bool playerInSight = directionToPlayer.magnitude <= 10.0f; // Ajuste o valor 5.0f conforme necess�rio para o seu jogo.

        // Se o jogador estiver no alcance de vis�o e a IA n�o estiver em um ataque de dash,
        // realiza o ataque de dash em dire��o ao jogador.
        if (playerInSight && !isDashing)
        {
            StartDashAttack(directionToPlayer.normalized);
        }

        // Controla o tempo do ataque de dash.
        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashCooldown)
            {
                isDashing = false;
                dashTimer = 0.0f;
            }
        }
    }

    void StartDashAttack(Vector2 dashDirection)
    {
        if (!isDashing)
        {
            animator.SetBool("IsDashing", true);
            // Aplica uma for�a para realizar o ataque de dash.
            GetComponent<Rigidbody2D>().velocity = dashDirection * dashSpeed;
            isDashing = true;
        }
        else
        {
            animator.SetBool("IsDashing", false);
        }
    }
    public void Damage(float damageAmount)
    {
       // Hit();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // O barril colidiu com o jogador. Reinicie o jogo recarregando a cena.
            RestartGame();
        }
    }

    void RestartGame()
    {
        // Recarregue a cena atual (voc� deve configurar a cena no Unity).
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

