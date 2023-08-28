using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeInvisivel : MonoBehaviour
{

    public int contagemInv;
    public int maxcontagemInv;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("graves"))
        {
            // grave tocou no objeto
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"));
            Invoke("voltar", 2f);
            //spriteRenderer.enabled = false;

        }
    }


    void voltar()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"), false);
       // spriteRenderer.enabled = true;

    }

}
