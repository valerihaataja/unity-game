using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    EnemyAI enemyAI;

    void Start()
    {
        health = 100f;
        enemyAI = transform.GetComponent<EnemyAI>();
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            enemyAI.Death();
        }
    }
}
