using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DemonPatrolling : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    List<Transform> patrolLocations = new List<Transform>();

    public bool allowedToPatrol, aggroTowardPlayer;

    private int patrolIndex = 0;

    private DemonFOV demonFOVRef;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        demonFOVRef = this.gameObject.GetComponent<DemonFOV>();

    }
    private void Start()
    {


        if (allowedToPatrol && patrolLocations.Count == 0)
        {
            Debug.LogError($"{gameObject.name}, does not have any patrol points and has been set to patrol");
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (allowedToPatrol)
        {
            
            StartPatrolling(patrolLocations);
        }


    }


    void StartPatrolling(List<Transform> incomingLocations)
    {
        if ((!agent.pathPending && agent.remainingDistance < .5f) && !demonFOVRef.canSeePlayer)
        {
            agent.destination = incomingLocations[patrolIndex].position;
            patrolIndex = (patrolIndex + 1) % incomingLocations.Count;
        }
        else
        {
            ChasePlayer(aggroTowardPlayer);
        }
    
    }
    void ChasePlayer(bool shouldChasePlayer)
    {
        if (shouldChasePlayer)
        {
            if (demonFOVRef.canSeePlayer)
            {
                agent.destination = demonFOVRef.playerRef.transform.position;               
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
}
