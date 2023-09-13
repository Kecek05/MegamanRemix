using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehavior : MonoBehaviour, Itouchable
{
    private AudioSource TP;
    public string level;
    [SerializeField] private AudioSource portal;

    // Start is called before the first frame update
    private void Start()
    {
        TP = GetComponent<AudioSource>();
    }



    public void touch()
    {
        portal.Play();
        CompleteLeve1();
        

    }

    private void CompleteLeve1()
    {
        LoadBehaviour.LoadLevel(level);
    }
}
