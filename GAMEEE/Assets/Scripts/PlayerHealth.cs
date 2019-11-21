using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float health;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            
        }
    }
}
