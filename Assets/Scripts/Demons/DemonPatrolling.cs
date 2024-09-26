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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        if(patrolLocations.Count == 0) 
        {
            Debug.LogWarning($"{gameObject.name}, does not have any patrol points");
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        if(patrolLocations.Count != 0)
        {
            StartPatrolling(patrolLocations);
        }
        
        
    }


    void StartPatrolling(List<Transform> incomingLocations)
    {
        if(!agent.pathPending && agent.remainingDistance < .5f)
        {
            agent.destination = incomingLocations[patrolIndex].position;
            patrolIndex = (patrolIndex + 1) % incomingLocations.Count;
        }
       
        
    }



    
    
}
