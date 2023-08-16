using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleFollow : MonoBehaviour, IDamageable
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    public int lives = 2;
    private float distance;
   
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }


    public void Damage(float damageAmount)
    {
        Hit();

    }

    public void Hit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnParticleCollision(GameObject other)
    {
        lives--;
     

        if (lives < 1)
        {
            Destroy(gameObject, 1f);
        }
    }
}
