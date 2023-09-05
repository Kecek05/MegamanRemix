using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeBlinkBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private int delayBlink;

    private void Update()
    {
        Invoke("BlinkOn", delayBlink);
    }


    void BlinkOn()
    {
        gameObject.SetActive(false);
        Invoke("BlinkOff", delayBlink);
    }
    void BlinkOff()
    {
        gameObject.SetActive(true);
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
