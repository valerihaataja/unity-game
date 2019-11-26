using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
//  public float fireRate = 15f;
    public Camera fpsCam;
    public GameObject Holder;
    public int selected;
    public ParticleSystem Flash;
    public AudioSource shotSound;

    public GameObject impactEffect;

    EnemyHealth enemyHealth;

    //private float nextTimeToFire = 0f;


    
    void Update()
    {
     
        

        if (Input.GetButtonDown("Fire1"))///Time.time >= nextTimeToFire)
        {
          //nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
        
        
    }
    void Shoot()
    {

        shotSound.Play();
        Flash.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            if(hit.transform.tag == "Enemy")
            {
                enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                enemyHealth.takeDamage(damage);
            }
            else
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.takeDamage(damage);
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }

            GameObject impactGO =Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
  
       
    }


