using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoBarrilBehaviour : MonoBehaviour
{

    private Animator anim;
    private string currentState;
    const string ESQUELETO_IDLE = "Esqueleto_Idle";
    const string MINOTAURO_ATTACK = "Esqueleto_Attack";
    const string MINOTAURO_DEATH = "";

    public GameObject player; // Refer�ncia ao jogador.
    public GameObject barrelPrefab; // Prefab do barril a ser lan�ado.
    public float throwForce = 10.0f; // For�a do lan�amento do barril.
    public float throwingInterval = 3.0f; // Intervalo de tempo entre os lan�amentos.                                              

    private Transform throwPoint;
    private bool canThrow = true;


    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }


    void Start()
    {
        throwPoint = transform.Find("ThrowPoint"); // Ponto de onde o barril ser� lan�ado.
        StartCoroutine(ThrowBarrelsContinuously());

           // spriteRenderer = GetComponent<SpriteRenderer>();
           // rb2d = GetComponent<Rigidbody2D>();
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
