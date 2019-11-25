using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looting : MonoBehaviour
{
    public GameObject LootBox;
    public GameObject Player;
    public GameObject Gun;
    public GameObject Panel;
    public float distance_;








    // Update is called once per frame
    void Update()
    {
        Animator animator = Panel.GetComponent<Animator>();
        distance_ = Vector3.Distance(LootBox.transform.position, Player.transform.position);
        if(Panel != null)
        {
            if (distance_ < 4)
            {
               
                if (animator != null)
                {
                    bool isOpen = animator.GetBool("open");
                    animator.SetBool("open", true);
  
                }
            }
            if (distance_ > 4)
            {
                animator.SetBool("open", false);

            }

        }

        if (distance_ < 3)
        {
            
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("Looting!");
                Gun.gameObject.SetActive(true);
           


            }
            
        }
       
    }
}
