using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    public GameObject player; // Refer�ncia ao jogador.
    public GameObject barrelPrefab; // Prefab do barril a ser lan�ado.
    public float throwForce = 10.0f; // For�a do lan�amento do barril.
    public float movementSpeed = 3.0f; // Velocidade de movimento da IA.
    public float stoppingDistance = 2.0f; // Dist�ncia para parar ao chegar perto do jogador.

    private Transform throwPoint;
    private bool canThrow = true;

    void Start()
    {
        throwPoint = transform.Find("ThrowPoint"); // Ponto de onde o barril ser� lan�ado.
    }

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Movimenta a IA em dire��o ao jogador apenas se a dist�ncia for maior que a dist�ncia de parada.
        if (distanceToPlayer > stoppingDistance)
        {
            // Movimenta a IA em dire��o ao jogador.
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
        }

        // Verifica se o jogador est� dentro do alcance de vis�o da IA.
        if (distanceToPlayer <= stoppingDistance && canThrow)
        {
            // Lan�a o barril na dire��o do jogador.
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
            // Calcula a dire��o para o jogador.
            Vector2 direction = (player.transform.position - throwPoint.position).normalized;

            // Aplica a for�a para lan�ar o barril na dire��o do jogador.
            barrelRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);

            // Impede que a IA jogue barris continuamente em curtos intervalos.
            canThrow = false;
            StartCoroutine(ResetThrowCooldown());
        }
    }

    IEnumerator ResetThrowCooldown()
    {
        // Ap�s 1 segundo, permite que a IA jogue barris novamente.
        yield return new WaitForSeconds(1.0f);
        canThrow = true;
    }
}
