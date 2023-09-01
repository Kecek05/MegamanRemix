using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolinha : MonoBehaviour, IDamageable
{


    public float speed = 20f;
    public Rigidbody2D rb;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        //rb.velocity = transform.right * speed;
        if (player != null)
        {
            Vector2 playerPosition = player.position;


            if (playerPosition.x < transform.position.x)
            {
                FireBullet(Vector2.left);
            }
            else
            {
                FireBullet(Vector2.right);
            }
        }
    }


    void FireBullet(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
   
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(gameObject);
    }

     public void Damage(float damageAmount)
    {
         Hit();
        print("morra!");
    }
    private void Hit()
    {
        Destroy(gameObject);
    }
}