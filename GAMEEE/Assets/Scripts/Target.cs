using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject destroyedVersion;

    public void takeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
