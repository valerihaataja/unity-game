using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public GameObject Door;
    public GameObject Player;
    public GameObject DistanceCheck;
    public float dis;
    public Camera fpsCamera;
    public float range = 4f;
    Animator animator;
    public GameObject DoorOpenSound;




    void Update()
    {
        animator = Door.GetComponent<Animator>();
        dis = Vector3.Distance(DistanceCheck.transform.position, Player.transform.position);

        if (Input.GetKeyDown("e") && range > 0)
        {
            CheckObject();
        }

        if (dis > 4f)
        {
            animator.SetBool("open", false);
     
        }

        if (dis > 6f)
        {
            DoorOpenSound.gameObject.SetActive(false);

        }







    }

    void CheckObject()
    {

        //Door 
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Door2")
            {
                Debug.Log(hit.transform.name);
                if (animator != null)
                {
                    DoorOpenSound.gameObject.SetActive(true);
                    animator.GetBool("open");
                    animator.SetBool("open", true);
                }

            }


        }

    }
}