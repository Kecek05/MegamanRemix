using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Square : MonoBehaviour, Itouchable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void touch()
    {

        Tocar();
        print("estou tentando");

    }
    public void Tocar()
    {
        print("Tocou");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
