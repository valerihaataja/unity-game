using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleScript : MonoBehaviour
{

    public GameObject greenlight;
    public GameObject redlight;
    public GameObject player;
    public GameObject console;
    public float dis;
    public Camera fpsCamera;
    AudioSource clickSound;
    public GameObject secretDoor;
    public GameObject secretDoorSound;
    // Start is called before the first frame update
    void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(console.transform.position, player.transform.position);
        if(Input.GetKeyDown("e") && dis < 5f)
        {
            pressConsole();
        }
    }

    void pressConsole()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit))
        {
            if(hit.transform.tag == "console")
            {
                clickSound.Play();
                greenlight.gameObject.SetActive(false);
                redlight.gameObject.SetActive(true);
                secretDoor.gameObject.SetActive(false);
                secretDoorSound.SetActive(true);
                Invoke("soundOff", 5f);
            }
            if(hit.transform.tag == "console2")
            {
                clickSound.Play();
                GameObject door2 = GameObject.Find("Door (1)");
                LockedDoor lockedDoor = door2.GetComponent<LockedDoor>();
                lockedDoor.isUnlocked = true;
                greenlight.gameObject.SetActive(false);
                redlight.gameObject.SetActive(true);

            }
        }
    }

    void soundOff()
    {
        secretDoor.gameObject.SetActive(false);
        secretDoorSound.gameObject.SetActive(false);
   
    }
}
