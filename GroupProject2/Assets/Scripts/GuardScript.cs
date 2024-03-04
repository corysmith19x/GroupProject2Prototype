using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardScript : MonoBehaviour
{
    NavMeshAgent agent;
    MusicScript musicScript;
    public Transform player;
    Vector3 origin;

    float sightRange = 15f;
    float catchRange = 1.2f;

    public float chaseSpeed, patrolSpeed;
    bool inSightRange;
    public bool investigating = false;
    bool stunned = false;

    //Cory's addition:
    public Animator animator;
    bool isIdle;
    public GameObject iM;


    // Start is called before the first frame update
    void Start()
    {
        iM = GameObject.Find("InterfaceManager");
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
        musicScript = GameObject.FindObjectOfType<MusicScript>();
        //Animator
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < sightRange)
        {
            inSightRange = true;
        }
        else inSightRange = false;

        if (!investigating && !stunned)
        {
            if (inSightRange)
            {
                ChasePlayer();
            }
            else
            {
                ReturnToOrigin();
                musicScript.Alone();
            }
        }
        else
        {
            if(inSightRange)
            {
                investigating = false;
            }
            else if(Vector3.Distance(agent.destination, transform.position) < 1.4f)
            {
                investigating = false;
            }
        }

        if (distance < catchRange)
        {
            //the guard caught you. make the lose state, aka the scene that shows when you lose.
            iM.GetComponent<InterfaceManager>().gameOver();
        }

        
    }

    void ChasePlayer()
    {
        musicScript.Chase();
        agent.SetDestination(player.position);
        //Animations
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", true);
    }

    void ReturnToOrigin()
    {
        agent.SetDestination(origin);
        //Animations
        animator.SetBool("isChasing", false);
    }

    public void InvestigateSound(Vector3 spot)
    {
        investigating = true;
        musicScript.Nearby();
        StartCoroutine("InvestigateCooldown");
        agent.SetDestination(spot);
        //Animations
        animator.SetBool("isIdle", false);
    }

    public IEnumerator InvestigateCooldown()
    {
        yield return new WaitForSeconds(5);
        investigating = false;
        //Animations
        yield return new WaitForSeconds(5);
        animator.SetBool("isIdle", true);
    }

    public IEnumerator Stunned()
    {
        stunned = true;
        animator.SetBool("isStunned", true);
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(5);
        stunned = false;
        animator.SetBool("isStunned", false);
    }
}
