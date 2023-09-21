using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour, IDamageable
{

    private Animator anim;
    private string currentState;
    const string FIRE = "Fire";

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("enemy"));
        ChangeAnimationState(FIRE);
        Destroy(this.gameObject);
    }


    void Update()
    {
        
    }

    public void Damage(float damageAmount)
    {
        //hit();
    }
}
