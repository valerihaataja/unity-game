using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;
    Animator anim;
    public EnemySight enemySight;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > 2 && distance < 20)
        {
            agent.updatePosition = true;
            agent.SetDestination(target.position);
            anim.SetBool("Walk", true);
        }
        else
        {
            agent.updatePosition = false;
            anim.SetBool("Walk", false);
        }
    }
}
