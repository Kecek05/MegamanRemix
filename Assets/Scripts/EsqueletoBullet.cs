using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoBullet : MonoBehaviour
{
    public float initialSpeed = 5.0f; // Velocidade inicial do tiro do inimigo
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Inicialize a velocidade inicial do tiro
        rb.velocity = transform.right * initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
