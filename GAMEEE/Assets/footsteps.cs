using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public CharacterController cc;
    public AudioSource audioSource;
    bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetState();
        PlayAudio();
    }
    public void GetState(){
        if(cc.isGrounded && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl))
            {
                isRunning = false;
            }else
            {
                isRunning = true;
            }
        }else isRunning = false;
    }

    public void PlayAudio(){
        if(isRunning){
            audioSource.loop = true;
            audioSource.pitch = Random.Range(0.7f, 1.0f);
            audioSource.volume = Random.Range(0.1f, 0.5f);
            audioSource.enabled = true;
        }else {
            audioSource.loop = false;
            audioSource.enabled = false;
        }
    }
}
