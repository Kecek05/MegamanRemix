using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleFollow : MonoBehaviour, IDamageable
{
    public GameObject target;
    Rigidbody2D rdb;
    // Start is called before the first frame update
    void Start()
    {
        rdb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 dif = target.transform.position - transform.position;
            rdb.AddForce(dif);
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
}
