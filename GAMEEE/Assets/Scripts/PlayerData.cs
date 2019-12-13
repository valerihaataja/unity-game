using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData 
{
    public float health;
    public string level;
    public bool[] guns;

    public PlayerData (PlayerHealth playerHealth)
    {
        health = playerHealth.health;
        level = SceneManager.GetActiveScene().name;

    }

    public PlayerData (Looting looting)
    {
        for (int i = 0; i < looting.guns.Length; i++)
        {
            if (looting.guns[i].activeSelf)
            {
                guns[i] = true;
            }
        }
    }

}
