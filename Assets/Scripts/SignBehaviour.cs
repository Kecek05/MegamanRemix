using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBehaviour : MonoBehaviour
{
    public GameObject textoPlaca;

    //private bool jogadorPerto = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //jogadorPerto = true;
            textoPlaca.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //jogadorPerto = false;
            textoPlaca.SetActive(false);
        }
    }

    //private void Update()
    //{
    //    if (jogadorPerto && Input.GetKeyDown(KeyCode.E)) 
    //    {
           
    //        Debug.Log("Jogador interagiu com a placa!");
    //    }
    //}
}
