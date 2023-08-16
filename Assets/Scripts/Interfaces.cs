using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour
{

}
    public interface IDamageable
    {
        void Damage(float damageAmount);
    }
    
    public interface Itouchable
    {
    void touch();
    }

