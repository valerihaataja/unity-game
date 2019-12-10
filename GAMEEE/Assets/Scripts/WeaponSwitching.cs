using UnityEngine;
using UnityEngine.UI;
public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Text weaponText;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        selectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
      
        int previousSelectedWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }


        if (previousSelectedWeapon != selectedWeapon)
        {
            selectWeapon();
        }
        setWeapons();
    }

    public void selectWeapon()
    {
  


            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);

                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
    }

    void setWeapons()
    {
        if(transform.childCount == 0)
        {
            weaponText.text = "No weapons";
        }else
               if (selectedWeapon == 0)
                {

                  //  transform.GetChild(0).gameObject.SetActive(true);
                    weaponText.text = "Plasma Pistol";
                }
                if ( selectedWeapon == 1)
                {

                  //  transform.GetChild(1).gameObject.SetActive(true);
                    weaponText.text = "Rifle";
                }
                if (selectedWeapon == 2)
                {
                   // transform.GetChild(2).gameObject.SetActive(true);
                    weaponText.text = "Heavy";
                }
                
    }

   
   
 
   
}
