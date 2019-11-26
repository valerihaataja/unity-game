using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashLightScript : MonoBehaviour
{
    public GameObject FlashLight;
    bool LightisOn = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (LightisOn == false)
            {
                FlashLight.gameObject.SetActive(true);
                LightisOn = true;
            }
            else if (LightisOn == true)
            {
                FlashLight.gameObject.SetActive(false);
                LightisOn = false;
            }

        }
    }
}
