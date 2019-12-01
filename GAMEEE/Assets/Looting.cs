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
    public float dis;
    Animator animator;
    public Camera fpsCamera;
    public float range = 4f;
    public bool hasSeen = false;
    public int lootedWeapons = 1;
    



    // Update is called once per frame
    void Update()
    {
        animator = Panel.GetComponent<Animator>();
        dis = Vector3.Distance(LootBox.transform.position, Player.transform.position);
        
        if (Input.GetKeyDown("e") && range > 0)
        {
            CheckLoot();
        }
        if (hasSeen == false)
        {
            PanelAnimation();
        }
        if (dis > 4)
        {
            animator.SetBool("open", false);
            if(dis > 8 && hasSeen == true)
            {
                Panel.gameObject.SetActive(false);
            }
           
        }

    }

    void CheckLoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if(hit.transform.tag == "Loot")
            {
                Gun.SetActive(true);
                Debug.Log("Looting!");
                GameObject weapons = GameObject.Find("WeaponHolder");
             
                    weapons.transform.GetChild(0).gameObject.SetActive(true);
                    weapons.transform.GetChild(1).gameObject.SetActive(false);
                    weapons.transform.GetChild(2).gameObject.SetActive(false);
                
                
            }
           
        }
    }
    void PanelAnimation()
    {
     
        if (Panel != null)
        {
            if (dis < 4)
            {

                if (animator != null)
                {
                    animator.GetBool("open");
                    animator.SetBool("open", true);
                    hasSeen = true;
                }
            }
           

        }
        

    }
}
