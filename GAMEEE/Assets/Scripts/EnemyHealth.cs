using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    EnemyAI enemyAI;
    public RectTransform healthbar;
    private bool boss = false;

    void Start()
    {
        health = 100f;
        enemyAI = transform.GetComponent<EnemyAI>();
        if (transform.name == "Titan")
        {
            boss = true;
        }
    }


    public void takeDamage(float amount)
    {
        if(boss)
        {
            health -= amount / 6f;
        }else
        {
            health -= amount;
        }


        if (health <= 0)
        {
            enemyAI.Death();
            health = 0f;
        }

        healthbar.sizeDelta = new Vector2(health * 2, healthbar.sizeDelta.y);
        healthbar.parent.parent.gameObject.SetActive(true);

    }
}
