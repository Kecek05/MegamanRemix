using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EsqueletoBullet: MonoBehaviour

{

    public GameObject TiroEsqueletoPrefab; // Prefab do proj�til

    public Transform EsqueletoTiroSpawn;         // Ponto de onde o proj�til ser� disparado

    public float fireForce = 10f;       // For�a do disparo

    public float angle = 45f;           // �ngulo de lan�amento

    public float fireRate = 2f;         // Taxa de disparo em segundos



    private float nextFireTime;



    void Start()

    {

        nextFireTime = Time.time + fireRate;

    }



    void Update()

    {

        if (Time.time >= nextFireTime)

        {

            FireProjectile();

            nextFireTime = Time.time + fireRate;

        }

    }



    void FireProjectile()

    {

        GameObject newProjectile = Instantiate(TiroEsqueletoPrefab, EsqueletoTiroSpawn.position, Quaternion.identity);

        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();



        // Converter �ngulo para radianos

        float angleRad = angle * Mathf.Deg2Rad;



        // Calcular componentes x e y da velocidade inicial

        float initialVelocityX = fireForce * Mathf.Cos(angleRad);

        float initialVelocityY = fireForce * Mathf.Sin(angleRad);



        // Definir a velocidade inicial do proj�til

        rb.velocity = new Vector2(initialVelocityX, initialVelocityY);

    }

}