using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    //private NavMeshAgent nav;
    private CapsuleCollider col;
    private Animator anim;
    //private LastPlayerSighting lastPlayerSighting;
    private GameObject player;
    private Vector3 previousSighting;

    void Awake()
    {
       // nav = GetComponent<NavMeshAgent>();
        col = GetComponent<CapsuleCollider>();
        //lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<lastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag("Player");

        //personalLastSighting = lastPlayerSighting.resetPosition;
        //previousSighting = lastPlayerSighting.resetPosition;
    }

    void Update()
    {
        //if(lastPlayerSighting.position != previousSighting){
           // personalLastSighting = lastPlayerSighting.position;
        //}
        //previousSighting = lastPlayerSighting.position;
        
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if(angle < fieldOfViewAngle * 0.5f) {
                RaycastHit hit;
                if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    if(hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        //lastPlayerSighting.position = player.transform.position;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject == player)
        {
            playerInSight = false;
        }
    }
}
