using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardScript : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    Vector3 origin;

    float sightRange = 20f;
    //float catchRange = 1.2f;

    public float chaseSpeed, patrolSpeed;
    bool inSightRange;
    public bool investigating = false;
    bool stunned = false;
	
	public GameObject dangerVolume;
	
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
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
				dangerVolume.SetActive(true);
                ChasePlayer();
            }
            else 
			{
				dangerVolume.SetActive(false);
				ReturnToOrigin();
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
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void ReturnToOrigin()
    {
        agent.SetDestination(origin);   
    }

    public void InvestigateSound(Vector3 spot)
    {
        investigating = true;
        StartCoroutine("InvestigateCooldown");
        agent.SetDestination(spot);
    }

    public IEnumerator InvestigateCooldown()
    {
        yield return new WaitForSeconds(10);
        investigating = false;
    }

    public IEnumerator Stunned()
    {
        stunned = true;
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(4);
        stunned = false;
    }
}
