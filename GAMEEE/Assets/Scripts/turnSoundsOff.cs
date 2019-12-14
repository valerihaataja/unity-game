using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnSoundsOff : MonoBehaviour
{

    public GameObject[] sounds;
    public GameObject player;
    public GameObject trigger;
    public GameObject music;
    public Rigidbody rb;
    public GameObject roarSound;
    // Start is called before the first frame update
  
    private void FixedUpdate()
    {


        for (int i = 0; i < sounds.Length; i++)
        {
            if (player.transform.position.z > trigger.transform.position.z)
            {
                sounds[i].gameObject.SetActive(false);
                rb.AddForce(300f * Time.deltaTime, 0, 0);

                music.gameObject.SetActive(true);
                roarSound.gameObject.SetActive(true);
                Invoke("turnOff", 8f);
            }
            else
            {
                sounds[i].gameObject.SetActive(true);
               
            }


        }
       
    }
    void turnOff()
    {
        roarSound.gameObject.SetActive(false);
    }
   
    
}