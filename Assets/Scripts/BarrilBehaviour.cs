using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarrilBehaviour : MonoBehaviour, IDamageable
{
    public float rollingSpeed = 5.0f; // Velocidade de rota��o do barril.
    public float moveSpeed = 2.0f; // Velocidade de movimento do barril.
    private GameObject player;
    private bool isRolling = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("O barril precisa ter um componente Rigidbody2D para funcionar corretamente.");
        }
    }

    void Update()
    {
        if (player != null && isRolling)
        {
            // Calcula a dire��o para o jogador.
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Aplica uma for�a para fazer o barril rolar na dire��o do jogador.
            rb.velocity = direction * moveSpeed;

            // Rota��o do barril.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    public void SetPlayer(GameObject targetPlayer)
    {
        player = targetPlayer;
        isRolling = true;
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

    public void Damage(float damageAmount)
    {
        //hit


    }
}
