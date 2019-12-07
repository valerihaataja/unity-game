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
    public GameObject[] guns;
    public GameObject WeaponHolder;
    public GameObject LootLight;
    bool hasLooted = false;
    public int gunId = 0;
    public Text weaponText;
    public Text newWeaponText;
    public GameObject lootsoundObj;
    bool hasPlayed = false;
    Animator newWeapon;
    

    private void Start()
    {
        newWeaponText.gameObject.SetActive(false);
        newWeapon = newWeaponText.GetComponent<Animator>();
       
    }
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

            if (hit.transform.tag == "Loot")
            {

                if(hasPlayed == false)
                {
                    newWeaponText.gameObject.SetActive(true);
                    lootsoundObj.SetActive(true);
                    Invoke("shutDownSound", 4f);
                    newWeapon.Play("newGunAnim");
                    Invoke("shutDownAnimation", 3f);
                }
               
                guns[gunId].transform.parent = WeaponHolder.transform;
                guns[gunId].SetActive(true);
                Gun.SetActive(true);
                Debug.Log("Looting!");
                hasLooted = true;
                if(gunId == 0)
                {
                    weaponText.text = "Plasma Pistol";
                }
                if(gunId == 1)
                {
                    weaponText.text = "Rifle";
                }
                if(gunId == 2)
                {
                    weaponText.text = "Rifle";
                }
                
                
                //guns[2].transform.parent = WeaponHolder.transform;
                //GameObject.Find("WeaponHolder").GetComponent<WeaponSwitching>().selectWeapon();
            }
           if(hasLooted == true)
            {
                LootLight.gameObject.SetActive(false);     
            }
        }
    }

 
    void PanelAnimation()
    {
     
        if (Panel != null)
        {
            if (dis < 4)
            {
                Panel.SetActive(true);

                if (animator != null)
                {
                    animator.GetBool("open");
                    animator.SetBool("open", true);
                    hasSeen = true;
                }
            }
           

        }
        

    }
    void shutDownSound()
    {
        Destroy(lootsoundObj);
        hasPlayed = true;
    }
    void shutDownAnimation()
    {
        newWeaponText.gameObject.SetActive(false);
        hasPlayed = true;
    }
    
}
