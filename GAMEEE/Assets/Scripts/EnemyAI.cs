using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public Transform[] patrolPoints;
    public int destPoint = 0;

    Transform target;
    NavMeshAgent agent;

    public float fieldOfViewAngle = 110f;
    public float perceptionDistance = 20f;

    public bool playerInSight;
    public float distance;

    private Vector3 lastSightedLocation;

    private Transform childObject;
    private LineRenderer objectLineRenderer;

    public enum State
    {
        PATROL,
        CHASE,
        INVESTIGATE
    }

    public State state;
    private bool alive;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastSightedLocation = transform.position;

        state = EnemyAI.State.PATROL;

        alive = true;

        childObject = GameObject.Find("Laser").transform;
        objectLineRenderer = childObject.GetComponent<LineRenderer>();
        objectLineRenderer.enabled = false;


        StartCoroutine("FSM");
        //anim = GetComponent<Animator>();
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.PATROL:
                    Patrol();
                    break;
                case State.CHASE:
                    Chase();
                    break;
                case State.INVESTIGATE:
                    Investigate();
                    break;
            }
            yield return null;
        }
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        Vision();
    }

    void Patrol()
    {
        agent.autoBraking = false;

        if (patrolPoints.Length == 0)
        {
            return;
        }
        agent.destination = patrolPoints[destPoint].position;
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            destPoint = (destPoint + 1) % patrolPoints.Length;
        }
    }

    void Chase()
    {
        objectLineRenderer.enabled = true;
        agent.destination = target.position;
        if(!playerInSight)
        {
            state = EnemyAI.State.INVESTIGATE;
        }
    }

    void Investigate()
    {
        objectLineRenderer.enabled = false;
        agent.destination = lastSightedLocation;
        if (agent.remainingDistance < 0.5f)
        {
            state = EnemyAI.State.PATROL;
        }
    }

    void Vision()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up * 0.5f, direction.normalized, out hit, perceptionDistance))
            {
                Debug.DrawLine(transform.position + Vector3.up * 1.36f, hit.point, Color.green);
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = EnemyAI.State.CHASE;
                    playerInSight = true;
                    agent.isStopped = false;
                    agent.updatePosition = true;
                    
                    lastSightedLocation = hit.point;
                    //anim.SetBool("Walk", true);
                }
                else
                {
                    playerInSight = false;
                    // agent.velocity = Vector3.zero;
                    // agent.isStopped = true;
                    // agent.updatePosition = false;
                    // anim.SetBool("Walk", false);
                }
            }
        }
    }
}
