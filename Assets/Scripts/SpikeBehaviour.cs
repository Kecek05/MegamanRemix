using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeBehaviour : MonoBehaviour, IDamageable
{
    private CircleCollider2D crcoll2D;
    private Rigidbody2D rgdb2D;
    private BoxCollider2D boxColl;
    private SpriteRenderer spriteRenderer;
    private bool objetoTocado;
    private bool estadoInv;

    [SerializeField] private float transparencia;

    public GameObject[] objectsToIgnore;
    void Start()
    {
        objetoTocado = false;
        estadoInv = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider2D>();
        rgdb2D = GetComponent<Rigidbody2D>();
        crcoll2D = GetComponent<CircleCollider2D>();

    }

    void Update()
    {

        //if (estadoInv)
        //{

        //    // Tornar o objeto invisível
        //    SwitchInv();
        //}
        //else
        //{
        //    // Tornar o objeto visível
        //    Invoke("SwitchInv", 1f);

        //}
    }


    void SwitchInv()
    {
        if (estadoInv)
        {
            // Tornar o objeto invisível
            spriteRenderer.enabled = false;

            //boxColl.enabled = false;
        }
        else
        {
            //Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("graves"), false);
            spriteRenderer.enabled = true;
            //boxColl.enabled = true;
            Invoke("Saiu", 1f);
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

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("graves"))
    //    {
    //        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"));
    //        // O inimigo tocou no objeto
    //        objetoTocado = true;
    //        spriteRenderer.enabled = false;

    //    }
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("graves"))
        {
            scamGraves();
            // grave tocou no objeto
            spriteRenderer.enabled = false;
            //Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"));
            //Physics.IgnoreCollision(GetComponent<Collider2D>(), objectCollider);
            
            if (objectsToIgnore.Length > 0)
            {
                foreach (GameObject objectToIgnore in objectsToIgnore)
                {
                    // Obtém uma referência ao colisor 2D do objetoToIgnore
                    Collider2D objectCollider = objectToIgnore.GetComponent<Collider2D>();

                    // Verifica se o colisor foi encontrado
                    if (objectCollider != null)
                    {
                        // Ignora a colisão entre este objeto e o colisor do objetoToIgnore
                        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), objectCollider);
                        print("ignorado");
                        print(objectsToIgnore.Length);
                    }
                    else
                    {
                        Debug.LogWarning("Um objeto com a tag especificada não possui um Collider2D para ignorar.");
                    }
                    
                }
                print("to fora");
                print(objectsToIgnore.Length);
                Invoke("voltar", 0.5f);

            }
        }
        print("objetos " + objectsToIgnore.Length);
    }
    

    public void scamGraves()
    {
        objectsToIgnore = GameObject.FindGameObjectsWithTag("graves");
        print(objectsToIgnore.Length);
    }

    void voltar()
    {
        //Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"), false);
        spriteRenderer.enabled = true;

    }

    void Saiu()
    {
        //Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"), false);
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("graves"))
    //    {

    //        // O inimigo parou de tocar no objeto
    //        estadoInv = false;
    //        StateInv();


    //    }
    //}


    public void Damage(float damageAmount)
    {
        Hit();
        
    }


    public void Hit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
