using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlatBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Espinhos"));
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Espinhos"));
    }
}
