using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject porta;

    [SerializeField] private string nomeLayerGrave;

    [SerializeField] private AudioSource openPorta;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(nomeLayerGrave))
        {
            if (!openPorta.isPlaying)
            {
                openPorta.Play();
            }
            porta.SetActive(false);
    
        }
    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(nomeLayerGrave))
        {
            if (!openPorta.isPlaying)
            {
                openPorta.Play();
            }
            porta.SetActive(true);
        }
    }
}
