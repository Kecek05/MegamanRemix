using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour , IDamageable
{
    public GameObject player; // Referência ao jogador.
    public GameObject barrelPrefab; // Prefab do barril a ser lançado.
    public float throwForce = 10.0f; // Força do lançamento do barril.
    public float throwingInterval = 3.0f; // Intervalo de tempo entre os lançamentos.                                              

    private Transform throwPoint;
    private bool canThrow = true;

    void Start()
    {
        throwPoint = transform.Find("ThrowPoint"); // Ponto de onde o barril será lançado.
        StartCoroutine(ThrowBarrelsContinuously());
    }

    public void Damage(float damageAmount)
    {
       // Hit();

    }

    void Update()
    {
        // A IA não se move mais em direção ao jogador.
        // O código de movimentação foi removido aqui.
    }

    IEnumerator ThrowBarrelsContinuously()
    {
        while (true)
        {
            if (player == null || barrelPrefab == null || throwPoint == null)
                yield break;

            // Lança o barril na direção do jogador.
            ThrowBarrelAtPlayer();

            // Espera pelo intervalo antes de lançar o próximo barril.
            yield return new WaitForSeconds(throwingInterval);
        }
    }

    void ThrowBarrelAtPlayer()
    {
        GameObject barrel = Instantiate(barrelPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D barrelRigidbody = barrel.GetComponent<Rigidbody2D>();

        if (barrelRigidbody != null)
        {
            // Calcula a direção para o jogador.
            Vector2 direction = (player.transform.position - throwPoint.position).normalized;

            // Aplica a força para lançar o barril na direção do jogador.
            barrelRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }
    }
}
