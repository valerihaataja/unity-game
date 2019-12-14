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

    Animator anim;

    public float fieldOfViewAngle = 110f;
    public float perceptionDistance = 20f;
    public float hearingDistance = 20f;
    public float hitDistance = 4f;

    private float damage = 30f;

    public bool playerInSight;
    public float distance;

    private bool hitInProgress = false;

    private Vector3 lastSightedLocation;

    PlayerHealth playerHealth;
    footsteps playerMovement;

    private bool boss = false;

    private AudioSource[] audioSources;
    public GameObject lizardArmy;

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

        playerHealth = target.GetComponent<PlayerHealth>();
        playerMovement = target.GetComponent<footsteps>();

        state = EnemyAI.State.PATROL;


        alive = true;

        if(transform.name == "Titan")
        {
            boss = true;
            hitDistance = 6f;
            damage = 40f;
            audioSources = GetComponents<AudioSource>();
        }

        //childObject = GameObject.Find("Laser").transform;
        //objectLineRenderer = childObject.GetComponent<LineRenderer>();
        //objectLineRenderer.enabled = false;

        //childObject = GameObject.Find("MuzzleFlashEffect").transform;
        //particleSystem = childObject.GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();

        StartCoroutine("FSM");
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
        if(alive)
        {
            distance = Vector3.Distance(transform.position, target.position);
            Vision();
            Hearing();
        }
    }

    void Patrol()
    {
        agent.autoBraking = false;

        anim.SetBool("IsWalking", true);
        anim.SetBool("IsRunning", false);

        if (patrolPoints.Length == 0)
        {
            return;
        }
        agent.destination = patrolPoints[destPoint].position;
        if(!agent.pathPending && agent.remainingDistance < 3f)
        {
            destPoint = (destPoint + 1) % patrolPoints.Length;
        }
    }

    void Chase()
    {
        //objectLineRenderer.enabled = true;
        agent.destination = target.position;
        anim.SetBool("IsRunning", true);
        agent.speed = 7f;
        
        //particleSystem.Play();
        //GameObject.Find("MuzzleFlashEffect").transform.GetComponent<ParticleSystem>().Play();

        if(distance < hitDistance)
        {
            stopMovement();
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("HasPunched", true);
        }
        else if (distance > hitDistance && !hitInProgress)
        {
            //resumeMovement();
            anim.SetBool("HasPunched", false);
        }

        if (!playerInSight)
        {
            state = EnemyAI.State.INVESTIGATE;
        }
    }

    void Investigate()
    {
        //particleSystem.Stop();
        anim.SetBool("IsWalking", true);
        anim.SetBool("IsRunning", false);

       // objectLineRenderer.enabled = false;
        agent.destination = lastSightedLocation;
        if (agent.remainingDistance < 1f)
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
               // Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Player")
                {
                    if(state == EnemyAI.State.INVESTIGATE || state == EnemyAI.State.PATROL)
                    {
                        
                    }
                    state = EnemyAI.State.CHASE;
                    if(!hitInProgress)
                    {
                        playerInSight = true;
                        agent.isStopped = false;
                        agent.updatePosition = true;
                    }

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

    public void HitEvent()
    {
        Debug.Log("Player hit!");
        if (distance < hitDistance)
        {
            playerHealth.takeDamage(damage);
        }
    }

    public void Death()
    {
        if(alive)
        {
            alive = false;
            agent.velocity = Vector3.zero;
            agent.updatePosition = false;
            agent.isStopped = true;
            anim.SetTrigger("IsDead");
            transform.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject, 10);
        }
    }

    void Hearing()
    {
        //Jos pelaaja liikkuu nopeasti tai ampuu
        if(state != EnemyAI.State.CHASE && playerMovement.isRunning)
        {
            NavMeshPath navMeshPath = new NavMeshPath();
            if (agent.enabled)
            {
                agent.CalculatePath(target.position, navMeshPath);
            }
            Vector3[] allWayPoints = new Vector3[navMeshPath.corners.Length + 2];

            allWayPoints[0] = transform.position;
            allWayPoints[allWayPoints.Length - 1] = target.position;

            for (int i = 0; i < navMeshPath.corners.Length; i++)
            {
                allWayPoints[i + 1] = navMeshPath.corners[i];
            }

            float pathLength = 0f;

            for (int i = 0; i < allWayPoints.Length - 1; i++)
            {
                pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
            }

            if (pathLength < hearingDistance)
            {
                lastSightedLocation = target.position;
                state = EnemyAI.State.INVESTIGATE;
            }
        }
    }

    public void stopMovement()
    {
        agent.velocity = Vector3.zero;
        agent.updatePosition = false;
        agent.isStopped = true;
        hitInProgress = true;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void resumeMovement()
    {
        agent.updatePosition = true;
        agent.isStopped = false;
        hitInProgress = false;
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Roar()
    {
        audioSources[1].Play();
        lizardArmy.gameObject.SetActive(true);
    }

}
