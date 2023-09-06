using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeBlinkBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private int delayBlinkOn;
    [SerializeField] private int delayBlinkOff;
    [SerializeField] private int delayStart;


    public float transparencia;
    public float duracaoPiscada;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxColl;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider2D>();
        Invoke("BlinkOn", delayStart);
    }


    void BlinkOn()
    {
        StartCoroutine(Piscar());
        boxColl.enabled = false;
 
        Invoke("BlinkOff", delayBlinkOff);
    }
    void BlinkOff()
    {
        boxColl.enabled = true;
        Invoke("BlinkOn", delayBlinkOn);
    }
    public void Damage(float damageAmount)
    {
        Hit();

    }


    public void Hit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

}
