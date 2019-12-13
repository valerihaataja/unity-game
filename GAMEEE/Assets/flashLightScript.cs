using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashLightScript : MonoBehaviour
{
    public GameObject FlashLight;
    bool LightisOn = false;
    AudioSource clickSound;

    private void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (LightisOn == false)
            {
                clickSound.Play();
                FlashLight.gameObject.SetActive(true);
                LightisOn = true;
            }
            else if (LightisOn == true)
            {
                clickSound.Play();
                FlashLight.gameObject.SetActive(false);
                LightisOn = false;
            }

        }
    }
}
