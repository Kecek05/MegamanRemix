using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject porta;

    [SerializeField] private string nomeLayerGrave;





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(nomeLayerGrave))
        {
            porta.SetActive(false);
            print("Morcego");
        }
        print("trigger");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(nomeLayerGrave))
        {
            porta.SetActive(true);
        }
    }
}
