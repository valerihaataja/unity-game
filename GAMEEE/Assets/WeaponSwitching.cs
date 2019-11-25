using UnityEngine;
using UnityEngine.UI;
public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Text weaponText;
   
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }


        if (previousSelectedWeapon != selectedWeapon)
        {
            selectWeapon();
        }
        printText();
    }

    public void selectWeapon()
    {
        
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
                
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    void printText()
    { 

        if (selectedWeapon == 0)
        {
            weaponText.text = "Plasma Pistol";
           
        }
        if (selectedWeapon == 1)
        {
           weaponText.text = "Rifle";
           
        }
        if (selectedWeapon == 2)
        {
           weaponText.text = "Heavy";
        }
    }
   
   
}
