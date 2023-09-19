using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilBehaviour : MonoBehaviour, IDamageable
{
    public float moveSpeed = 2.0f; // Velocidade de movimento do barril.
    private GameObject player;
    //private bool isRolling = false;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("O barril precisa ter um componente Rigidbody2D para funcionar corretamente.");
        }
        //Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"));

        if (player != null)
        {
            // Calcula a direção para o jogador.
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Aplica uma força para fazer o barril rolar na direção do jogador.
            rb.velocity = direction * moveSpeed;

            // Rotação do barril.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BarrilLimit")
        {
            DestroyThis();
        }
    }
    public void Damage(float damageAmount)
    {
        //hit
        DestroyThis();

    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
