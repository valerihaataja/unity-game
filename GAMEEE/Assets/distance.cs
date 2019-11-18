using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distance : MonoBehaviour
{
    public GameObject Door;
    public GameObject Player;
    public float dis;
    bool hasOpened = false;

   
    void Update()
    {
        dis = Vector3.Distance(Door.transform.position, Player.transform.position);
        if(dis < 4 )
        {
           
            Door.SetActive(false);
            hasOpened = true;
       
           
            
        }else 
        {
 
            Door.SetActive(true);
        }
    }
}
