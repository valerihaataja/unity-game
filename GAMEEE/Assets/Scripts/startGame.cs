﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Return) && SceneManager.GetActiveScene().name == "Credits")
        {
            Application.Quit();
        }
        
    }

    public void Play()
    {
        SaveSystem.DeleteData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}