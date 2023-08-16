using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    private AudioSource TP;

    // Start is called before the first frame update
    private void Start()
    {
        TP = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //finishSound.Play();
            //CompleteLeve1();
        }
    }

    private void CompleteLeve1()
    {

    }
}
