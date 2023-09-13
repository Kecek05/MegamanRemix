using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalNoLoad : MonoBehaviour, Itouchable
{



    // Start is called before the first frame update
    private void Start()
    {

    }



    public void touch()
    {

        CompleteLeve1();


    }

    private void CompleteLeve1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
