using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBehaviour : MonoBehaviour
{
    public GameObject textoPlaca;
    public string mensagem = "Texto da placa aqui";

    private bool jogadorPerto = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = true;
            textoPlaca.SetActive(true);
            textoPlaca.GetComponentInChildren<TextMesh>().text = mensagem;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorPerto = false;
            textoPlaca.SetActive(false);
        }
    }

    private void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E)) // Pode usar uma tecla diferente se preferir
        {
            // Implemente qualquer ação que deseja que ocorra quando o jogador interage com a placa aqui
            Debug.Log("Jogador interagiu com a placa!");
        }
    }
}
