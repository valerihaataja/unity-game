using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public ParticleSystem Flash;
    AudioSource shotSound;
    Animator animator;
    public GameObject impactEffect;
    public GameObject Weapon;
    EnemyHealth enemyHealth;

    private float nextTimeToFire = 0f;



    void Start()
    {
        shotSound = GetComponent<AudioSource>();
        animator = Weapon.GetComponent<Animator>();

    }

    void Update()
    {
        

        if (Input.GetButton("Fire1")&&Time.time >= nextTimeToFire && transform.tag != "Pistol")
        {
          nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            if (transform.tag == "Rifle")
            {
                animator.Play("rifleShootAnim");
            }
            if(transform.tag == "Heavy")
            {
                animator.Play("heavyShootAnim");
            }
        }
       
        if(Input.GetButtonDown("Fire1") && transform.tag == "Pistol")
        {
            Shoot();
            animator.Play("shootAnim");
        }
        
    }
    void Shoot()
    {
   
       
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
        shotSound.Play();
        Flash.Play();
    }
        


    }
