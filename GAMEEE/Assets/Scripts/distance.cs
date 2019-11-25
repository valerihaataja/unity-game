using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distance : MonoBehaviour
{
    public GameObject Door;
    public GameObject Player;
    public float dis;
   


    void Update()
    {
        Animator animator = Door.GetComponent<Animator>();
        
        dis = Vector3.Distance(Door.transform.position, Player.transform.position);
     
            if (Input.GetKeyDown("e") && dis < 4)
            {
                if (animator != null)
                {

                bool hasOpened = animator.GetBool("open");
                animator.SetBool("open", true);
    
                }
            }
            if (dis > 4)
            {
                animator.SetBool("open", false);
        
                
            }
        
      
    }
}

