using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeBehaviour : MonoBehaviour, IDamageable
{

    private SpriteRenderer spriteRenderer;


    public float transparencia;
    public float duracaoPiscada = 0.2f;

    public GameObject[] objectsToIgnore;
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();


    }

 




    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("graves"))
        {
            scamGraves();
            // grave tocou no objeto
            //spriteRenderer.enabled = false;
            //Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("grave"));
            //Physics.IgnoreCollision(GetComponent<Collider2D>(), objectCollider);

            StartCoroutine(Piscar());

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

    private IEnumerator Piscar()
    {
        // Reduz a transparência
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, transparencia);

        // Aguarda a duração do piscar
        yield return new WaitForSeconds(duracaoPiscada);

        // Restaura a transparência original do jogador
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }



    public void Damage(float damageAmount)
    {
        //Hit();
        
    }


    public void Hit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
