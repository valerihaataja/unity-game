using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public float health;
    public GameObject damageIndicator;
    public float damageDuration = 4f;
    private float fadeTime = 2f;
    Image image;
    public RectTransform healthbar;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        image = damageIndicator.GetComponent<Image>();
        HideDamageIndicator();
        LoadData();
        healthbar.sizeDelta = new Vector2(health * 4, healthbar.sizeDelta.y);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        ShowDamageIndicator();
        StartCoroutine(FadeOut());
       // CancelInvoke("HideDamageIndicator");
       // Invoke("HideDamageIndicator", damageDuration);
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        healthbar.sizeDelta = new Vector2(health * 4, healthbar.sizeDelta.y);
    }
    public void ShowDamageIndicator()
    {
        damageIndicator.SetActive(true);
    }

    public void HideDamageIndicator()
    {
        damageIndicator.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color color = image.color;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = 0.5f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = color;
            yield return null;
        }
    }

    public void saveData()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("saved");
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
        {
            health = data.health;
        }

    }
}
