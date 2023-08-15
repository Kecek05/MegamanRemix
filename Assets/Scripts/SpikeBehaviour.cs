using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeBehaviour : MonoBehaviour, IDamageable
{
    void Start()
    {
        
    }
    void Update()
    {
        
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
