using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public GameObject HealthBar;
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void Start()
    {
        HealthBar.SetActive(false);
    }

    public void HealthToggle(bool state)
    {
        HealthBar.SetActive(state);
    }
}
