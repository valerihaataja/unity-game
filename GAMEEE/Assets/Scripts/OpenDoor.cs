﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public GameObject Player;
    public GameObject DistanceCheck;
    public GameObject Panel;
    public float dis;
    public Camera fpsCamera;
    public float range = 4f;
    Animator animator;
    Animator panelAnimator;
    public bool hasSeen = false;


    void Update()
    {
        animator = Door.GetComponent<Animator>();
        panelAnimator = Panel.GetComponent<Animator>();
        dis = Vector3.Distance(DistanceCheck.transform.position, Player.transform.position);
        
        if (Input.GetKeyDown("e") && range > 0)
        {
             CheckObject();
        }
        if(hasSeen == false)
        {
            PanelAnimation();
        }
        if (dis > 4)
        {
            panelAnimator.SetBool("open", false);
            animator.SetBool("open", false);
            if(dis > 8 && hasSeen == true)
            {
                Panel.gameObject.SetActive(false);
            }
        }

    }
    void CheckObject()
    {
        
        //Door 
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Door")
            {
                
                Debug.Log(hit.transform.name);
                if (animator != null)
                {

                    animator.GetBool("open");
                    animator.SetBool("open", true);
                }

            }
        
            
        }
        
    }
    void PanelAnimation()
    {

       
            if (dis < 4)
            {

                if (panelAnimator != null)
                {
                    panelAnimator.GetBool("open");
                    panelAnimator.SetBool("open", true);
                    hasSeen = true;
                }
            }

       

    }

}
