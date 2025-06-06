using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DemonPatrolling : MonoBehaviour
{
    private NavMeshAgent agent; // agent for demon

    [SerializeField]
    List<Transform> patrolLocations = new List<Transform>(); // List of locations to patrol to 

    public bool allowedToPatrol, aggroTowardPlayer; // should demon chase the player or patrol

    private int patrolIndex = 0;  // Keeps track of which patrol point in list

    private DemonFOV demonFOVRef;  // References the demonPOV script for seeing the player
    private void Awake()
    {
        //if the demon has a NavMeshAgent attached to itself it will give the agent a value, if not it will create one
        agent =  GetComponent<NavMeshAgent>() != null ? GetComponent<NavMeshAgent>(): gameObject.AddComponent<NavMeshAgent>(); 
        
        //Gives the demonFOVRef a value
        demonFOVRef = this.gameObject.GetComponent<DemonFOV>();

    }
    private void Start()
    {

        //If the demon is allowed to patrol but doesn't have any locations to patrol to will display error in console
        if (allowedToPatrol && patrolLocations.Count == 0)
        {
            Debug.LogError($"{gameObject.name}, does not have any patrol points and has been set to patrol");
        }

    }

    // Update is called once per frame
    void Update()
    {

        //If demon can patrol, patrols
        if (allowedToPatrol)
        {            
            StartPatrolling(patrolLocations);
        }


    }


    void StartPatrolling(List<Transform> incomingLocations) //Method for patrolling demon functionality
    {
        if ((!agent.pathPending && agent.remainingDistance < .5f) && !demonFOVRef.canSeePlayer) //IF the demon's path isn't pending and is close to location move to next point
                                                                                                // as long as it doesn't see player
        {
            agent.destination = incomingLocations[patrolIndex].position;
            patrolIndex = (patrolIndex + 1) % incomingLocations.Count;
        }
        else
        {
            ChasePlayer(aggroTowardPlayer); //If demon does see player activates ChasePlayer function
        }
    
    }
    void ChasePlayer(bool shouldChasePlayer) //Method for chasing player, that takes a bool
    {
        if (shouldChasePlayer) //Uses the reference form the demonPOV script and if it does see the player chases it
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
