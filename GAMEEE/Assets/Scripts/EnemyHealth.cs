using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    EnemyAI enemyAI;
    public RectTransform healthbar;

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
            health = 0f;
        }

        healthbar.sizeDelta = new Vector2(health * 2, healthbar.sizeDelta.y);
        healthbar.parent.parent.gameObject.SetActive(true);

    }
}
