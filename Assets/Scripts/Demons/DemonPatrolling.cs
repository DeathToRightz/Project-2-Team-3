using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DemonPatrolling : MonoBehaviour
{
    /*[SerializeField]
    Camera mainCamera;*/

    private NavMeshAgent agent;

    [SerializeField]
    List<Transform> patrolLocations = new List<Transform>();
    bool shouldPatrol;
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
        /*if(Input.GetMouseButtonDown(0)) 
        {
           Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //agent.SetDestination(hit.point); //Smooth move to click point
                
                Debug.Log(hit.point);
            }
        }*/ ///When person click on screen and hits something where the NavMeshAgent can go it will go there

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
