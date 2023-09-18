using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour , IDamageable
{
    public GameObject player; // Refer�ncia ao jogador.
    public GameObject barrelPrefab; // Prefab do barril a ser lan�ado.
    public float throwForce = 10.0f; // For�a do lan�amento do barril.
    public float throwingInterval = 3.0f; // Intervalo de tempo entre os lan�amentos.                                              

    private Transform throwPoint;
    private bool canThrow = true;

    void Start()
    {
        throwPoint = transform.Find("ThrowPoint"); // Ponto de onde o barril ser� lan�ado.
        StartCoroutine(ThrowBarrelsContinuously());
    }

    public void Damage(float damageAmount)
    {
       // Hit();

    }

    void Update()
    {
        // A IA n�o se move mais em dire��o ao jogador.
        // O c�digo de movimenta��o foi removido aqui.
    }

    IEnumerator ThrowBarrelsContinuously()
    {
        while (true)
        {
            if (player == null || barrelPrefab == null || throwPoint == null)
                yield break;

            // Lan�a o barril na dire��o do jogador.
            ThrowBarrelAtPlayer();

            // Espera pelo intervalo antes de lan�ar o pr�ximo barril.
            yield return new WaitForSeconds(throwingInterval);
        }
    }

    void ThrowBarrelAtPlayer()
    {
        GameObject barrel = Instantiate(barrelPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D barrelRigidbody = barrel.GetComponent<Rigidbody2D>();

        if (barrelRigidbody != null)
        {
            // Calcula a dire��o para o jogador.
            Vector2 direction = (player.transform.position - throwPoint.position).normalized;

            // Aplica a for�a para lan�ar o barril na dire��o do jogador.
            barrelRigidbody.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }
    }
}
