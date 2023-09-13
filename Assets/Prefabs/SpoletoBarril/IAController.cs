using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    public GameObject player; // Referência ao jogador.
    public GameObject barrelPrefab; // Prefab do barril a ser lançado.
    public float throwForce = 10.0f; // Força do lançamento do barril.
    public float movementSpeed = 3.0f; // Velocidade de movimento da IA.
    public float stoppingDistance = 2.0f; // Distância para parar ao chegar perto do jogador.

    private Transform throwPoint;
    private bool canThrow = true;

    void Start()
    {
        throwPoint = transform.Find("ThrowPoint"); // Ponto de onde o barril será lançado.
    }

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Movimenta a IA em direção ao jogador apenas se a distância for maior que a distância de parada.
        if (distanceToPlayer > stoppingDistance)
        {
            // Movimenta a IA em direção ao jogador.
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
        }

        // Verifica se o jogador está dentro do alcance de visão da IA.
        if (distanceToPlayer <= stoppingDistance && canThrow)
        {
            // Lança o barril na direção do jogador.
            ThrowBarrelAtPlayer();
        }
    }

    void ThrowBarrelAtPlayer()
    {
        if (barrelPrefab == null || throwPoint == null)
            return;

        GameObject barrel = Instantiate(barrelPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D barrelRigidbody = barrel.GetComponent<Rigidbody2D>();

        if (barrelRigidbody != null)
        {
            // Calcula a direção para o jogador.
            Vector2 direction = (player.transform.position - throwPoint.position).normalized;

            // Aplica a força para lançar o barril na direção do jogador.
            barrelRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);

            // Impede que a IA jogue barris continuamente em curtos intervalos.
            canThrow = false;
            StartCoroutine(ResetThrowCooldown());
        }
    }

    IEnumerator ResetThrowCooldown()
    {
        // Após 1 segundo, permite que a IA jogue barris novamente.
        yield return new WaitForSeconds(1.0f);
        canThrow = true;
    }
}
