using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeInv : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spikeRender;
    private CircleCollider2D crcoll2D;
    private SpriteRenderer spriteRenderer;
    private bool objetoTocado;
    private bool estadoInv;
    void Start()
    {
        objetoTocado = false;
        estadoInv = false;
        spriteRenderer = spikeRender.GetComponent<SpriteRenderer>();
        crcoll2D = GetComponent<CircleCollider2D>();


        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("enemy"));

    }

    void Update()
    {

        if (estadoInv)
        {

            // Tornar o objeto invisível
            SwitchInv();
        }
        else
        {
            // Tornar o objeto visível
            Invoke("SwitchInv", 1f);

        }
    }


    void SwitchInv()
    {
        if (estadoInv)
        {
            // Tornar o objeto invisível
           // spriteRenderer.enabled = false;

            //boxColl.enabled = false;
        }
        else
        {
            //Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("graves"), false);
            //spriteRenderer.enabled = true;
            //boxColl.enabled = true;
           // Invoke("Saiu", 1f);
        }

    }

    void StateInv()
    {
        if (objetoTocado && !estadoInv)
        {
            estadoInv = true;

            print("entro");
        }
        else if (estadoInv == false)
        {
            estadoInv = false;
            print("saiu");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("graves"))
        {
            Invoke("Saiu", 3f);
            // O inimigo tocou no objeto
            //objetoTocado = true;
            //StateInv();

        }
    }

    void Saiu()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"), false);
        spriteRenderer.enabled = true;
        print("visivel");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("graves"))
        {

            // O inimigo parou de tocar no objeto
            estadoInv = false;
            StateInv();


        }
    }
}
