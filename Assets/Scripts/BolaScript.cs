using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaScript : MonoBehaviour, Itouchable
{
    public GameObject Porta;
    



    public void touch()
    {
        
        Porta.SetActive(false);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        Porta = GameObject.Find("PortaMinotauro");
        print(Porta);
    }

}
