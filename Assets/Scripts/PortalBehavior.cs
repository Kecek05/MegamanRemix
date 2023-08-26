using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehavior : MonoBehaviour, Itouchable
{
    private AudioSource TP;

    [SerializeField] private AudioSource portal;

    // Start is called before the first frame update
    private void Start()
    {
        TP = GetComponent<AudioSource>();
    }



    public void touch()
    {
        portal.Play();
        Invoke("CompleteLeve1", 2f);
        

    }

    private void CompleteLeve1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
