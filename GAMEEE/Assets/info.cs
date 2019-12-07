using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject panel;
    Animator animator;
    public float dis;
  

    // Update is called once per frame
    void Update()
    {
        animator = panel.GetComponent<Animator>();
        dis = Vector3.Distance(object1.transform.position, object2.transform.position);
       
         
        InfoAnimation();
         if(dis > 8f)
        {
            animator.SetBool("open", false);
            if(dis > 14f)
            {
                panel.gameObject.SetActive(false);
            }
           
                   
        }
    
        



    }
    void InfoAnimation()
    {

    
            if (dis < 8f)
            {
            panel.gameObject.SetActive(true);
                if (animator != null)
                {
                    animator.GetBool("open");
                    animator.SetBool("open", true);
                }
            }


    }
}
