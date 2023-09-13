using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
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
        }
    }

    public void SetPlayer(GameObject targetPlayer)
    {
        player = targetPlayer;
        isRolling = true;
    }

    // Implemente qualquer l�gica de colis�o e efeitos desejados aqui.
}
